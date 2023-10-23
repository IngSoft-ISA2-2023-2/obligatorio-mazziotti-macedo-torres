

namespace PharmaGo.WebApi.Models.In
{
    public class PurchaseModelRequest
    {

        public string BuyerEmail { get; set; }
        public DateTime PurchaseDate { get; set; }
        public ICollection<PurchaseDetailModelRequest> DrugsDetails { get; set; }
        public ICollection<PurchaseProductDetailModelRequest> ProductsDetails { get; set; }

        public class PurchaseDetailModelRequest {
            public string Code { get; set; }
            public int Quantity { get; set; }
            public int PharmacyId { get; set; }
        }

        public class PurchaseProductDetailModelRequest {
            public string Code { get; set; }
            public int Quantity { get; set; }
            public int PharmacyId { get; set; }
        }
    }
}
