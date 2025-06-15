# 🧠 Strategy Pattern
>#STRATEGY #FLEXIBILITY #PATTERN #YEY

## 💡 Conceptually
The Strategy Pattern enables selecting an algorithm’s behavior at runtime.
Instead of hardcoding logic, you define a family of interchangeable algorithms (or strategies), 
encapsulate them, and make them interchangeable through an interface.
>Think of it as “plug and play” logic — swap different behaviors without modifying the core object.

## ✅ Pros of Using the Strategy Pattern
- 🔀 Flexible Behavior Changes — Easily switch or extend behavior without modifying existing code. 
- 🧱 Solid Design — Adheres to Open/Closed Principle and promotes loose coupling. 
- ♻️ Reusability — Common logic is encapsulated in separate strategies that can be reused across multiple contexts. 
- 🧪 Testable — Each strategy can be tested independently.

## ❌ Cons of the Strategy Pattern
- 📦 More Classes — You’ll create multiple strategy classes for even simple logic. 
- 🤝 Requires DI or Context Management — You must manage which strategy is injected or selected at runtime. 
- ⚠️ Wrong Strategy Usage — Using an incorrect strategy may lead to unexpected behavior unless validated.

## 🛒 Strategy Pattern in This Project (Example: Quick Actions)
In this project, you use the pattern to perform insertion logic for different entities (Admin, Product, Dispatcher, etc.) 
in a decoupled and extensible way.

### 🎯 Core Components in This Project
#### IStrategy.cs
```csharp
using Weav_App.Models;

namespace Weav_App.Services.General.InsertStrategies;

public interface IStrategy<T>
{
    Task<ServiceResult<T>> CreateItem(T model);
}
```
#### AddAdminStrategy.cs
```csharp
public class AddAdminStrategy : IStrategy<RegisterUserModel>
{
    private readonly IAuthService _authenticationService;
    private readonly IMapper _mapper;

    public AddAdminStrategy(IAuthService authenticationService, IMapper mapper)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
    }
    public async Task<ServiceResult<RegisterUserModel>> CreateItem(RegisterUserModel model)
    {
        var modelDto = _mapper.Map<RegisterUserDto>(model);
        var(succes, error)  = await _authenticationService.RegisterAdmin(modelDto);

        if (succes == false)
        {
            return new ServiceResult<RegisterUserModel>
            {
                Success = false,
                Data = succes ? model : null,
                ErrorMessage = error
            };
        }

        return new ServiceResult<RegisterUserModel>
        {
            Success = true,
            Data = succes ? model : null,
            ErrorMessage = error
        };
    }
}
```

#### InsertProductStrategy.cs
```csharp
using AutoMapper;
using Weav_App.DTOs.Entities.Products;
using Weav_App.Models;
using Weav_App.Repositories.Interface;

namespace Weav_App.Services.General.InsertStrategies;
public class InsertProductStrategy : IStrategy<CreateProductModel>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public InsertProductStrategy(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ServiceResult<CreateProductModel>> CreateItem(CreateProductModel productModel)
    {
        var productDto = _mapper.Map<ProductDto>(productModel);
        var (success, error) = await _repo.CreateNewProduct(productDto, productModel.SelectedCategory);

        return new ServiceResult<CreateProductModel>
        {
            Success = success,
            Data = success ? productModel : null,
            ErrorMessage = error
        };
    }
}

```
#### InsertDispatcher.cs
```csharp
namespace Weav_App.Services.General.InsertStrategies;

public class InsertDispatcher
{
    private readonly IServiceProvider _provider;

    public InsertDispatcher(IServiceProvider provider)
    {
        _provider = provider;
    }

    public IStrategy<T> Resolve<T>()
    {
        return _provider.GetRequiredService<IStrategy<T>>();
    }
}

```
### 🧪 Example Usage
```csharp
var result = await _insertProductStrategy.CreateItem(model);
```
OR

```csharp
var newAdmin = await _insertProductStrategy.CreateItem(model);
```
## 💉 Register Strategies via DI in ASP.NET Core
```csharp
services.AddScoped<IStrategy<RegisterUserModel>, AddAdminStrategy>();
services.AddScoped<IStrategy<CreateProductModel>, InsertProductStrategy>();
```
## ✅ This is a textbook Strategy Pattern in practice:
Multiple algorithms → Implement same interface → 
Context selects which one to use at runtime → Swap behavior without changing the core code.

## 📦 Extendability
To add a new action like AddCustomerStrategy, just:
- Create a new class implementing IStrategy 
- Add to DI container 
- Dispatch with the new strategy name

_No need to modify existing logic. ✅_