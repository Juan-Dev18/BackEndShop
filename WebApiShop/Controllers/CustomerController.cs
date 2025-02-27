using Microsoft.AspNetCore.Mvc;
using WebApiShop.Interface;

namespace WebApiShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customerRepository;

        public CustomerController(ICustomer customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet(Name = "GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customer = await _customerRepository.GetCustomers();
            return Ok(customer);
        }
    }
}
