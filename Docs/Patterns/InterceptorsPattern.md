# 💡What are Interceptors?
> #DesignPattern #Patterns #State #Interceptors #Loggin #Authorization #Authentication
> 
> more info abt this pattern on this [site](https://medium.com/@octaviantuchila14/introduction-to-the-interceptor-pattern-899218598852)

***Interceptors*** are a ***design*** pattern that allows you to intercept and manipulate method calls, property access, 
or other actions before they reach their intended target. They enable you to add pre- and post-processing logic around 
these actions without modifying the actual business logic.

### 🧠 Conceptually:
- It "intercepts" calls or events before they reach their target. 
- It can modify or block the execution based on certain conditions.


![Interceptor Desgin Patter](https://www.researchgate.net/profile/T-Elrad/publication/220139630/figure/fig7/AS:305551293599750@1449860513595/Combined-State-and-Interceptor-Patterns.png)


## ✅ Pros of Using the Interceptor Pattern:
***Separation of Concerns:*** Logic like security, logging, validation, etc., is separated from business logic.

***Reusability:*** You can apply the same interceptor across many actions or controllers.

***Scalability:*** Easy to add or remove behaviors without modifying core logic.

***Centralized Control:*** Uniform enforcement of rules such as authorization or logging.

## ❌ Cons of Using the Interceptor Pattern:
***Complex Debugging:*** Logic is abstracted away, which may make debugging harder.

***Order of Execution:*** If multiple interceptors are used, managing the execution order can be tricky.

***Overhead:*** Too many interceptors can introduce performance overhead.

***Hidden Behavior:*** Interception might lead to implicit logic that isn't obvious when reading controller code.

## 🧪 How is the Interceptor Pattern Used in this project?

```csharp
    public class UserLevelAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly UserLevel _requiredLevel;

        public UserLevelAuthorizeAttribute(UserLevel level)
        {
            _requiredLevel = level;
        } 
        
        ...
        
        if (userLevel < _requiredLevel)
        {
            context.Result = new ForbidResult();
        }
    
            Console.WriteLine($"User level claim: {userLevelClaim}, parsed enum: {userLevel}");
        }
    }
```

### 🧩 Explanation:
- This code implements a custom interceptor using IAuthorizationFilter. 
- UserLevelAuthorizeAttribute intercepts controller requests before the action executes. 
- It checks if the user is authenticated and authorized to access the resource by evaluating their UserLevel claim. 
- If the check fails, it intercepts the request and halts further processing by setting context.Result = new ForbidResult().

### ✅ This is a classic authorization interceptor:
- The trigger is an HTTP request to a controller action. 
- The interceptor (OnAuthorization) validates user claims. 
- If the user is not allowed, it modifies the control flow (returns a forbidden result).

