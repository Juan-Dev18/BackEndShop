using Microsoft.AspNetCore.Mvc;
using WebApiShop.Interface;
using WebApiShop.Models;

namespace WebApiShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _orderRepository;

        public OrdersController(IOrder orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost(Name = "InsertOrder")]
        public async Task<IActionResult> InsertOrder([FromBody] ClsRequestCreateOrder request)
        {
            var orderResponse = await _orderRepository.InsertOrder(request);
            return Ok(orderResponse);
        }
    }
}
