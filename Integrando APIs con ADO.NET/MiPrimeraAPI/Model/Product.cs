namespace MiPrimeraAPI.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Stock { get; set; }
        public long UserId { get; set; }
    }
}
