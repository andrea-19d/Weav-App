# 🎨 What is the Decorator Pattern?
> #DesignPattern #Patterns #Decorator #FlexibleExtension #SOLID

The **Decorator Pattern** is a structural design pattern that allows behavior to be added to individual objects dynamically,
without affecting the behavior of other objects from the same class.

### 🧠 Conceptually:
- A decorator wraps an existing object.
- Adds new functionality before/after delegating the call to the original object.
- Keeps original class unchanged (Open/Closed Principle).

![Decorator Pattern](https://refactoring.guru/images/patterns/diagrams/decorator/structure.png)

---

## ✅ Pros of Using the Decorator Pattern:
**✔ Flexible Composition:** Easily mix and match functionalities using multiple decorators.  
**✔ No Subclass Explosion:** Avoids creating many subclasses for every feature combination.  
**✔ Open for Extension:** You can extend behavior without modifying the base class.  
**✔ Reusability:** Shared behavior can be added across multiple services/components.

---

## ❌ Cons of Using the Decorator Pattern:
**❌ Complexity:** Wrapping multiple decorators may be hard to trace/debug.  
**❌ Indirection:** Requires careful design to understand call flow.  
**❌ Constructor Management:** Injecting dependencies can get verbose or cluttered.

---

## 🧪 How is the Decorator Pattern Used in this project?

```csharp
public class OrderServiceLoggerDecorator : IOrderService
{
    private readonly IOrderService _inner;
    private readonly ILogger<OrderServiceLoggerDecorator> _logger;

    public OrderServiceLoggerDecorator(IOrderService inner, ILogger<OrderServiceLoggerDecorator> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<ServiceResult<List<OrdersListDTO>>> GetAllOrders()
    {
        _logger.LogInformation("Fetching all orders...");
        var result = await _inner.GetAllOrders();
        _logger.LogInformation($"Retrieved {result.Data.Count} orders.");
        return result;
    }

    // similar wrappers for other methods...
}
```
## 🧩 Explanation:
- This OrderServiceLoggerDecorator wraps an actual implementation of IOrderService. 
- It adds logging behavior before and after delegating the call to _inner.GetAllOrders(). 
- This allows you to plug in additional logic (like logging, caching, validation) without touching the original service.

## ✅ This is a classic service-layer decorator:
- The core service is injected as _inner. 
- Each method adds extra behavior while preserving the original method call. 
- Ideal for cross-cutting concerns such as logging, metrics, validation, or caching.
