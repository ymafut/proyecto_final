namespace MiPrimeraAPI.Controllers.DTOs
{
    public class PostProduct
    {
        public string Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        public long UserId { get; set; }
    }
}
