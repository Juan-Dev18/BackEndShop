namespace WebApiShop.Models
{
    public class ClsProduct
    {
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Stock { get; set; }
        public ClsCategory? Category { get; set; }
    }
}
