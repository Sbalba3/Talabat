using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.IRepository;
using Talabat.Core.Models;

namespace Talabat.Repository
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _database;

        public BasketRepo(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket=await _database.StringGetAsync(basketId);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var createdOrUpdated = await _database.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(1));
            if(!createdOrUpdated)return null;
            return await GetBasketAsync(customerBasket.Id);
        }
    }
}
