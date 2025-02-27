using WebApiShop.Interface;
using WebApiShop.Models;

namespace WebApiShop.Data
{
    public class Mutation
    {
        //Juan Carrasco: I have been researching how to use GraphQL and this seems to be one way to use it.

        private readonly IOrder _orderRepository;

        public Mutation(IOrder orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ClsRequestOrderResponse> InsertOrder(ClsRequestCreateOrder request)
        {
            return await _orderRepository.InsertOrder(request);
        }
    }
}
