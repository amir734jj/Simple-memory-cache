using System;
using System.Collections.Generic;
using AutoFixture;
using SimpleMemoryCache.Builders;
using Xunit;

namespace SimpleMemoryCache.Tests
{
    public class MemoryCacheTest
    {
        private readonly Fixture _fixture;

        public MemoryCacheTest()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void Test__Get()
        {
            // Arrange
            var cache = MemoryCacheBuilder.New()
                .Build();

            var keyValuePair = _fixture.Create<KeyValuePair<string, string>>();

            // Act
            cache.Set(keyValuePair.Key, keyValuePair.Value, TimeSpan.MaxValue);
            var result = cache.Get(keyValuePair.Key);

            // Assert
            Assert.Equal(keyValuePair.Value, result);
        }
        
        [Fact]
        public void Test__Delete()
        {
            // Arrange
            var cache = MemoryCacheBuilder.New()
                .Build();

            var keyValuePair = _fixture.Create<KeyValuePair<string, string>>();

            cache.Set(keyValuePair.Key, keyValuePair.Value, TimeSpan.MaxValue);

            // Act
            cache.Delete(keyValuePair.Key);
            var result = cache.Get(keyValuePair.Key);

            // Assert
            Assert.Equal(null, result);
        }
    }
}