# 💡Repository Pattern?

### 🧠 Conceptually:

The ***Repository Pattern*** is a design pattern that abstracts the data access
logic and provides a clean interface for the rest of the application to interact with the data source.
It acts like an in-memory collection, hiding the actual data source
(e.g., Supabase, SQL, MongoDB) and providing standard methods for querying, adding, updating, or deleting entities.

![Repository Diagram](https://asp.net/media/2578149/Windows-Live-Writer_8c4963ba1fa3_CE3B_Repository_pattern_diagram_1df790d3-bdf2-4c11-9098-946ddd9cd884.png)

> Think of it as a middleman between your business logic and your database.

![Repository Diagram](/Docs/Img/8ceaf081-fd99-4854-b6f4-840ba09cd621.png)

## ✅ Pros of Using the Interceptor Pattern:

+ 🔄 Separation of concerns — Keeps business logic (services) clean and focused.
+ 🧪 Testability — Makes unit testing easier by mocking repository interfaces.
+ 🔧 Flexibility — Switching data sources (e.g., from Supabase to EF Core) requires changes only in the repository layer.
+ ♻️ Reusability — Common operations (e.g., GetAll, FindById, Delete) can be reused across services.
+ 🔒 Encapsulation — Prevents business logic from directly accessing database internals.

## ❌ Cons of Using the Interceptor Pattern:

+ 🧱 Overhead — Adds an extra layer that may feel unnecessary for small applications.
+ 🔁 Boilerplate — Without a generic base class, there might be repetitive code.
+ 🔄 Duplication — If misused, repositories and services might overlap in responsibility.

## 🧪 How is the Interceptor Pattern Used in this project?

In this project, each entity (e.g., Product) has:

+ A repository interface: IProductRepository
```csharp
        public interface IProductRepository
        {
            Task<List<ProductDbTable>> GetAllAsync();
        }
```
+ A concrete implementation: ProductRepository
```csharp
      public class ProductRepository : IProductRepository
      {
      private readonly Client _supabase;

      public ProductRepository(Client supabase)
      {
          _supabase = supabase;
      }


      public async Task<List<ProductDbTable>> GetAllAsync()
      {
          if (_supabase == null)
              throw new Exception("Supabase client is NULL – check DI registration!");


          var result = await _supabase.From<ProductDbTable>().Get();
          return result.Models;
      }
```

+ A service: ProductService that uses the repository to perform business logic
```csharp
      public interface IProductServices
      {
          Task<ServiceResult<List<ProductDto>>> GetAllProducts();
      }
```
The repository encapsulates **Supabase** data access, so the service only focuses on business-level operations
(e.g., filtering, DTO mapping).

### 🧩 Explanation:

For example, instead of having this in the service:
```csharp
      var products = await supabase.From<ProductDbTable>().Get();
```
This allows your ProductService to:

+ Work only with domain models or DTOs
+ Remain ignorant of how/where the data is stored
+ Be easier to test (by mocking IProductRepository)

### ✅ This is a classic repository pattern setup:

> Controller → Service → Repository → SupabaseClient → Database

Each layer has one clear responsibility and is easily replaceable or mockable.
