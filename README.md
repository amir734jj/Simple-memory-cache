# Simple-memory-cache

Simple memory cache with an option to add persistency. To add persistancy, create a class that implements `IBasicCrud` and pass it to the `SetRefreshInterval` builder method.

```csharp
var cache = MemoryCacheBuilder.New()
    // Optional
    // .SetPersistancy( instance of class that implements IBasicCrud )
    .SetRefreshInterval(TimeSpan.FromSeconds(10))
    .Build();

// Set the cache key/value
cache.Set("some key", "some value", TimeSpan.FromMinutes(60));

// value == "some value"
var value = cache.get("some key");
```

## Operation time complexities:
- Initialization: `O(n)`
- Read: `O(1)`
- Write: `O(1)` + persistant `Write` call in a different thread`
- Delete: `O(1)` + persistant `Delete` call in a different thread`
