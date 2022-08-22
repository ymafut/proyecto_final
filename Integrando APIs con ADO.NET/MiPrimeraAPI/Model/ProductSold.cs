namespace MiPrimeraAPI.Model
{
    public class ProductSold
    {
        public long Id { get; set; }
        public int Stock { get; set; }
        public long ProductId { get; set; }
        public long SellId { get; set; }
    }
}
