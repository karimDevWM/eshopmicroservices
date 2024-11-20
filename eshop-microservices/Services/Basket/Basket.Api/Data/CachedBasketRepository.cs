
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

            // check if there is a basket that is stored into the cache
            if (!string.IsNullOrEmpty(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            
            // in case of there is no basket in cache, we retrieve the basket from database
            var basket = await basketRepository.GetBasket(userName, cancellationToken);
            
            // saving in cache the basket retrieved from database
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            
            // return the basket
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            // saving the basket into the database
            await basketRepository.StoreBasket(basket, cancellationToken);

            // saving the basket into the cache
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            // returning the basket
            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            // deleting the basket from the database
            await basketRepository.DeleteBasket(userName, cancellationToken);

            // deleting the basket from the cache
            await cache.RemoveAsync(userName, cancellationToken);

            // return true response
            return true;
        }
    }
}
