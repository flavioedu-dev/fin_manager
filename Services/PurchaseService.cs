using MongoDB.Driver;
using fin_manager.Models;
using fin_manager.Models.Interfaces;
using fin_manager.Services.Interfaces;
using System.Collections.Generic;


namespace fin_manager.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IMongoCollection<PurchaseModel> _purchases;

        public PurchaseService(IMongoConfiguration mongoConfig, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(mongoConfig.DatabaseName);
            _purchases = database.GetCollection<PurchaseModel>("purchases");
        }

        public List<PurchaseModel> GetPurchases()
        {
            try
            {
                return _purchases.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PurchaseModel GetPurchaseById(string id)
        {
            try
            {
                return _purchases.Find(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PurchaseModel> GetUserPurchases(List<string> purchasesId)
        {
            try
            {
                List <PurchaseModel> userPurchases = new List<PurchaseModel> ();

                foreach (string purchase in purchasesId)
                {
                    PurchaseModel existPurchase = _purchases.Find(x => x.Id == purchase).FirstOrDefault();
                    if (existPurchase == null) throw new Exception("Purchase not found.");
                    
                    userPurchases.Add(existPurchase);
                }

                return userPurchases;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PurchaseModel CreatePurchase(PurchaseModel purchase)
        {
            try
            {
                _purchases.InsertOne(purchase);

                return purchase;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdatePurchase(string id, PurchaseModel purchase)
        {
            try
            {
                _purchases.ReplaceOne(x => x.Id == id, purchase);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeletePurchase(string id)
        {
            try
            {
                _purchases.DeleteOne(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
