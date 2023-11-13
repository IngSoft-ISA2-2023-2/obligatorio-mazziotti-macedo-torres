namespace PharmaGo.Domain.Entities
{
    public class PurchaseProductDetail
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public string Status { get; set; }
    }
}