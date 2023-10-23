using PharmaGo.Domain.Entities;
using PharmaGo.WebApi.Models.In;

namespace PharmaGo.WebApi.Converters
{
    public class PurchaseModelRequestToPurchaseConverter
    {

        public Purchase Convert(PurchaseModelRequest model)
        {

            var purchase = new Purchase();
            purchase.PurchaseDate = model.PurchaseDate;
            purchase.BuyerEmail = model.BuyerEmail;
            purchase.details = new List<PurchaseDetail>();
            purchase.ProductDetails =  new List<PurchaseProductDetail>();
            if (model.DrugsDetails != null)
            {
                foreach (var detail in model.DrugsDetails)
                {
                    purchase.details
                        .Add(new PurchaseDetail
                        {
                            Quantity = detail.Quantity,
                            Drug = new Drug { Code = detail.Code },
                            Pharmacy = new()
                            {
                                Id = detail.PharmacyId
                            }
                        });
                }
            }
            if (model.ProductsDetails != null)
            {
                foreach (var productDetail in model.ProductsDetails)
                {
                    purchase.ProductDetails
                        .Add(new PurchaseProductDetail
                        {
                            Quantity = productDetail.Quantity,
                            Product = new Product { Code = productDetail.Code },
                            Pharmacy = new()
                            {
                                Id = productDetail.PharmacyId
                            }
                        });
                }
            }
            return purchase;
        }

    }
}
