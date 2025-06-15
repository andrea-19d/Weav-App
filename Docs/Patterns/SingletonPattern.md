# 🧩 Singleton Pattern
> #SINGLETON #SINGLE-LIFE #PATTERN #YEY
## 🧠 Conceptually
The Singleton Pattern ensures that a class has only one instance throughout the lifetime of an application and 
provides a global point of access to it.

In ASP.NET Core applications, it's commonly used via Dependency Injection (DI) when registering services that should 
maintain a single shared state — such as database clients, configuration providers, or logging engines.

> Think of it as a shared instance that all parts of your application can access without recreating it.

## ✅ Pros of Using the Singleton Pattern 
- 🧠 Memory efficiency — A single instance saves memory and avoids redundant setup.
- 🔄 Centralized configuration — Shared resources like the Supabase.Client can be initialized once and reused. 
- 🧪 Easy testing — The singleton instance can be mocked during unit testing. 
- 💉 Simplified dependency management — With DI, the singleton is automatically injected wherever it's needed.
- 🧱 Thread-safe lifecycle — In ASP.NET Core, AddSingleton ensures thread-safe creation of the object.

## ❌ Cons of Using the Singleton Pattern
- 🔓 Global state risks — If misused, singletons can introduce hidden dependencies and tight coupling. 
- 🔄 No per-request data — Not suitable for request-scoped or transient data (use Scoped or Transient instead). 
- 🤹 Harder to manage lifecycle manually — Especially when managing complex disposable resources.

## 🧪 How is the Singleton Pattern Used in This Project?
In this project, the Supabase.Client is configured using a singleton pattern to ensure a single 
connection instance across the application:

### ✅ Initialization:
```
var supabase = await SupabaseInitializer.InitializeSupabaseAsync();
builder.Services.AddSingleton(supabase);
```

This code:
- Creates a single initialized Supabase.Client 
- Registers it in the DI container using AddSingleton()

##  🔧 SupabaseInitializer
```csharp
public static class SupabaseInitializer
{
    public static async Task<Client> InitializeSupabaseAsync()
    {
        Env.Load(); // Loads from .env

        var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
        var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };

        var supabase = new Client(url, key, options);
        await supabase.InitializeAsync();

        return supabase;
    }
}

```

## 🧩 Explanation
Instead of creating a new Supabase.Client in every service or controller:
``` csharp
var supabase = new Client(...);
```
You inject the already-initialized singleton:
```csharp
public ProductRepository(Client supabase) { ... }
```

This leads to:
- Consistent connection state 
- Centralized error handling and logging 
- Less duplication and more maintainable code


## ✅This is a classic Singleton pattern setup via DI:
> Program.cs → Initializes Supabase.Client → Registers it as Singleton → Injects into 
> Repositories → Services → Controllers