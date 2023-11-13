using PharmaGo.Domain.Entities;

namespace PharmaGo.WebApi.Models.Out
{
    public class PurchaseProductDetailModelResponse
    {
        public int PurchaseId { get; set; }
        public int PurchaseProductDetailId { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public PurchaseProductDetailModelResponse(int id, PurchaseProductDetail productDetail)
        {
            PurchaseId = id;
            PurchaseProductDetailId = productDetail.Id;
            Status = productDetail.Status;
            Price = productDetail.Price;
            Quantity = productDetail.Quantity;
            PharmacyId = productDetail.Pharmacy.Id;
            PharmacyName = productDetail.Pharmacy.Name;
            ProductCode = productDetail.Product.Code;
            ProductName = productDetail.Product.Name;
        }
    }
}
