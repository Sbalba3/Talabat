using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.IRepository;
using Talabat.Core.Models;
using Talabat.Errors;

namespace Talabat.Controllers
{

    public class BasketsController : ApiBaseController
    {
        private readonly IBasketRepo _basketRepo;

        public BasketsController(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var basket=await _basketRepo.GetBasketAsync(id);
            return  basket is null? new CustomerBasket(id) : basket;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> EditBasketAsynk(CustomerBasket basket)
        {
            var createdOrUpdatedBasket=await _basketRepo.UpdateBasketAsync(basket);
            if (basket is null) return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdatedBasket);
            
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCustomerBasket(string id)
        {
            return await _basketRepo.DeleteBasketAsync(id);
        }
    }
}
