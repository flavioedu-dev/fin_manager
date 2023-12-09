using fin_manager.Models;

namespace fin_manager.Services.Interfaces
{
    public interface IPurchaseService
    {
        public List<PurchaseModel> GetPurchases();
        public PurchaseModel GetPurchaseById(string id);
        public PurchaseModel CreatePurchase(PurchaseModel purchase);
        public void UpdatePurchase(string id, PurchaseModel purchase);
        public void DeletePurchase(string id);
    }
}
