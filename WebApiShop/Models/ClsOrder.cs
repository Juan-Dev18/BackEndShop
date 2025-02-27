namespace WebApiShop.Models
{
    public class ClsOrder
    {
        public int? OrderId { get; set; }
        public ClsCustomer? Customer { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? OrderTotal { get; set; }
    }
}
