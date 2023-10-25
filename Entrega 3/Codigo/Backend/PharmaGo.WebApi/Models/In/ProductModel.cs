using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.In
{
    public class ProductModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string PharmacyName { get; set; }

        public Product ToEntity() => new()
        {
            Code = Code,
            Name = Name,
            Description = Description,
            Price = Price,
            Pharmacy = new Pharmacy() { Name = PharmacyName }
        };
    }
}
