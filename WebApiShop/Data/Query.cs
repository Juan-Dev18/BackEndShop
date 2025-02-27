using WebApiShop.Interface;
using WebApiShop.Models;

namespace WebApiShop.Data
{
    public class Query
    {
        //Juan Carrasco: I have been researching how to use GraphQL and this seems to be one way to use it.

        private readonly ICustomer _customerRepository;
        private readonly IProduct _productRepository;

        public Query(ICustomer customerRepository, IProduct productRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ClsCustomer>> GetCustomers()
        {
            return await _customerRepository.GetCustomers();
        }

        public async Task<IEnumerable<ClsProduct>> GetProducts(int offset, int pageSize)
        {
            return await _productRepository.GetProducts(offset, pageSize);
        }
    }
}
