using PharmaGo.Domain.Entities;
using static PharmaGo.WebApi.Models.In.PurchaseModelRequest;

namespace PharmaGo.WebApi.Models.Out
{
    public class PurchaseModelResponse
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string TrackingCode { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<PurchaseDetailModelResponseDuplicated>? Details { get; set; }
        public ICollection<PurchaseProductDetailModelResponseDuplicated> ProductDetails { get; set; }

        public class PurchaseDetailModelResponseDuplicated
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public int PharmacyId { get; set; }
            public string PharmacyName { get; set; }
            public string Status { get; set; }
        }

        public class PurchaseProductDetailModelResponseDuplicated
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public int PharmacyId { get; set; }
            public string PharmacyName { get; set; }
            public string Status { get; set; }
        }

        public PurchaseModelResponse(Purchase purchase)
        {
            Id = purchase.Id;
            BuyerEmail = purchase.BuyerEmail;
            TotalAmount = purchase.TotalAmount;
            PurchaseDate = purchase.PurchaseDate;
            TrackingCode = purchase.TrackingCode;
            Details = new List<PurchaseDetailModelResponseDuplicated>();
            if (purchase.details != null) {
                foreach (var detail in purchase.details) {
                    Details.Add(new PurchaseDetailModelResponseDuplicated {
                        Id = detail.Id,         
                        Name = detail.Drug.Name, 
                        Code = detail.Drug.Code, 
                        Price = detail.Drug.Price, 
                        Quantity = detail.Quantity,
                        PharmacyId = detail.Pharmacy.Id,
                        PharmacyName = detail.Pharmacy.Name,
                        Status = detail.Status
                });
                }
            }
            if (purchase.ProductDetails != null)
            {
                foreach (var productDetail in purchase.ProductDetails)
                {
                    ProductDetails.Add(new PurchaseProductDetailModelResponseDuplicated
                    {
                        Id = productDetail.Id,
                        Name = productDetail.Product.Name,
                        Code = productDetail.Product.Code,
                        Price = productDetail.Product.Price,
                        Quantity = productDetail.Quantity,
                        PharmacyId = productDetail.Pharmacy.Id,
                        PharmacyName = productDetail.Pharmacy.Name,
                        Status = productDetail.Status
                    });
                }
            }
        }
    }
}
