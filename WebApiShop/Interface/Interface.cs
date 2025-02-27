using WebApiShop.Models;

namespace WebApiShop.Interface
{

    //Juan Carrasco: Interface for Product and Order. I intend to use SOLID principles. At least the use of Interface Segregation Principle.

    public interface IProduct
    {
        Task<IEnumerable<ClsProduct>> GetProducts(int offset, int pageSize);
    }

    public interface IOrder
    {
        Task<ClsRequestOrderResponse> InsertOrder(ClsRequestCreateOrder orderInfo);
    }

    public interface ICustomer
    {
        Task<IEnumerable<ClsCustomer>> GetCustomers();
    }
}
