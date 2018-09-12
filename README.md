# Simple-memory-cache

```csharp
var cache = MemoryCacheBuilder.New()
    // Optional
    // .SetPersistancy( instance of class that extends IBasicCrud )
    .SetRefreshInterval(TimeSpan.FromSeconds(10))
    .Build();

// Set the cache key/value
cache.Set("some key", "some value", TimeSpan.FromMinutes(60));

// value == "some value"
var value = cache.get("some key");
```
