using AutoMapper;
using Supabase;
using Weav_App.DTOs.Entities.Categories;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Helpers;
using Weav_App.Repositories.Interface;

public class ProductRepository : IProductRepository
{
    private readonly Client _supabase;  
    private readonly IMapper _mapper;
    private readonly UsefulChecks _checks;
    private readonly NanoidGenerator _nanoidGenerator;
    
    public ProductRepository(Client supabase, IMapper mapper, UsefulChecks checks)
    {
        _supabase = supabase;
        _mapper = mapper;
        _checks = checks;
    }
    
    public async Task<List<ProductDbTable>> GetAllAsync()
    {
        if (_supabase == null)
            throw new Exception("Supabase client is NULL – check DI registration!");


        var result = await _supabase.From<ProductDbTable>().Get();
        return result.Models;
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
        // Step 1: Get the matching category
        var categoryResult = await _supabase
            .From<CategoryDbTable>()
            .Select("CategoryId")
            .Where(x => x.CategoryName == categoryName).Get();

        Console.WriteLine($"categoryName: {categoryResult.Model.CategoryId}");
        var category = categoryResult.Models.FirstOrDefault();
        if (category == null)
            return null;

        // Step 2: Get products with matching CategoryId
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