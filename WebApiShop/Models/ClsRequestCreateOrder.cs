namespace WebApiShop.Models
{
    public class ClsRequestCreateOrder
    {
        public ClsOrder? Order { get; set; }
        public List<ClsOrderDetail>? OrderDetails { get; set; }

    }
}
