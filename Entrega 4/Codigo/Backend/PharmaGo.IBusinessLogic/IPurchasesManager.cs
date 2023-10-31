using PharmaGo.Domain.Entities;

namespace PharmaGo.IBusinessLogic
{
    public interface IPurchasesManager
    {
        public ICollection<Purchase> GetAllPurchases(string token);
        public ICollection<Purchase> GetAllPurchasesByDate(string token, DateTime? start, DateTime? end);
        public Purchase CreatePurchase(Purchase purchase);
        public PurchaseDetail ApprovePurchaseDetail(int purchaseId, int pharmacyId, string drugCode);
        public PurchaseDetail RejectPurchaseDetail(int purchaseDetailId, int pharmacyId, string drugCode);
        public Purchase GetPurchaseByTrackingCode(string trackingCode);
        public PurchaseProductDetail RejectPurchaseProductDetail(int id, int pharmacyId, string productCode);
        public PurchaseProductDetail ApproveProductPurchaseDetail(int id, int pharmacyId, string productCode);
    }
}
