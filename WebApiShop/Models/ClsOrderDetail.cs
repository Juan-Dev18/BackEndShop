namespace WebApiShop.Models
{
    public class ClsOrderDetail
    {
        public int? OrderDetailId { get; set; }
        public ClsOrder? Order { get; set; }
        public ClsProduct? Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
