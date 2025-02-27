using Microsoft.AspNetCore.Mvc;
using WebApiShop.Interface;

namespace WebApiShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productRepository;

        public ProductController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts(int offset, int pageSize)
        {
            var products = await _productRepository.GetProducts(offset, pageSize);
            return Ok(products);
        }
    }
}
