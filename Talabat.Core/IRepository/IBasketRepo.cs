using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;

namespace Talabat.Core.IRepository
{
    public interface IBasketRepo
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
