﻿using AutoMapper;
using Supabase;
using Weav_App.DTOs.Entities.Categories;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Helpers;
using Weav_App.Models;
using Weav_App.Repositories.Interface;

public class ProductRepository : IProductRepository
{
    private readonly Client _supabase;  
    private readonly IMapper _mapper;
    private readonly UsefulChecks _checks;
    
    public ProductRepository(Client supabase, IMapper mapper, UsefulChecks checks)
    {
        _supabase = supabase;
        _mapper = mapper;
        _checks = checks;
    }

    public async Task<ErrorModel> DeleteProduct(int productId)
    {
        try
        {
            await _supabase.From<ProductDbTable>().Where(x => x.ProductId == productId).Delete();

            return new ErrorModel()
            {
                Message = "Product deleted",
                Status = true
            };
        }
        catch (Exception ex)
        {
            return new ErrorModel()
            {
                Message = ex.Message,
                Status = false
            }; 
        }
    }
    
    public async Task<List<ProductDbTable>> GetAllAsync()
    {
        if (_supabase == null)
            throw new Exception("Supabase client is NULL – check DI registration!");


        var result = await _supabase.From<ProductDbTable>().Get();
        return result.Models;
    }

    public async Task<ProductDbTable> GetProductByIdAsync(int id)
    {
        var result = await _supabase.From<ProductDbTable>()
            .Where(x => x.ProductId == id).Get();
        
        return result.Models.First();
    }

    
    public async Task<ErrorModel> UpdateProductAsync(ProductDbTable product)
    {
        var existingProduct = await GetProductByIdAsync(product.ProductId);
        
        if (existingProduct == null)
        {
            return new ErrorModel()
            {
                Message = "Product not found",
                Status = false
            };
        }
        
        
        try
        {
            _mapper.Map(product, existingProduct);
            existingProduct.UpdatedAt = DateTime.Now;
            
            var response = await _supabase
                .From<ProductDbTable>()
                .Where(x => x.ProductId == product.ProductId)
                .Update(existingProduct);

            
            Console.WriteLine("Product updated");
            return new ErrorModel()
            {
                Message = "Product updated",
                Status = true
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error model {ex.Message}");
            return new ErrorModel()
            {
                Message = ex.Message,
                Status = false
            };
        }
    }

    public async Task<List<ProductDbTable>> GetLowStokData()
    {
        var result = await _supabase.From<ProductDbTable>()
            .Where(x => x.Quantity <= 10).Get();
        
        return result.Models;
    }

    public async Task<List<ProductDbTable>> GetInactiveStokData()
    {
        var result = await _supabase.From<ProductDbTable>()
            .Where(x => x.Quantity == 0).Get();
        
        return result.Models;
    }

    public async Task<List<ProductDbTable>> GetProductByCategory(string categoryName)
    {
        var categoryResult = await _supabase
            .From<CategoryDbTable>()
            .Select("CategoryId")
            .Where(x => x.CategoryName == categoryName).Get();

        Console.WriteLine($"categoryName: {categoryResult.Model.CategoryId}");
        var category = categoryResult.Models.FirstOrDefault();
        if (category == null)
            return null;

        var productResult = await _supabase
            .From<ProductDbTable>()
            .Where(p => p.CategoryId == category.CategoryId)
            .Get();

        var products = productResult.Models;
        Console.WriteLine(products);

        return products;
    }

    public async Task<(bool succes, string? error)> CreateNewProduct(ProductDto productDto, string selectedCategory)
    {
        var categoryId = await _checks.GetCategoryByName(selectedCategory);

        var productDb = _mapper.Map<ProductDbTable>(productDto);
        var productExists = await _checks.RecordExistsAsync<ProductDbTable>("ProductName", productDb.ProductName);

        if (productExists != null)
        {
            return (false, "The product name already exist!");
        }
        
        productDb.CreatedAt = DateTime.Now;
        productDb.UpdatedAt = DateTime.Now;
        productDb.Barcode = NanoidGenerator.Generate(12);
        productDb.CategoryId = categoryId.CategoryId;

        if (productDb.ImageUrl == null)
        {
            productDb.ImageUrl = null;
        }
        
        try
        {
            Console.WriteLine(productDb.ProductId);
            await _supabase.From<ProductDbTable>().Insert(productDb);
            return (true, "Success");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
        
    }


}