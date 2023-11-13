﻿using PharmaGo.Domain.Entities;
using PharmaGo.Exceptions;
using PharmaGo.IBusinessLogic;
using PharmaGo.IDataAccess;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;


namespace PharmaGo.BusinessLogic
{
    public class PurchasesManager : IPurchasesManager
    {
        private readonly IRepository<Purchase> _purchasesRepository;
        private readonly IRepository<Pharmacy> _pharmacysRepository;
        private readonly IRepository<Drug> _drugsRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<PurchaseDetail> _purchaseDetailRepository;
        private readonly IRepository<PurchaseProductDetail> _purchaseProductDetailRepository;
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<User> _userRepository;

        private readonly string PENDING = "Pending";
        private readonly string REJECTED = "Rejected";
        private readonly string APPROVED = "Approved";

        public PurchasesManager(IRepository<Purchase> purchasesRepository,
                                IRepository<Pharmacy> pharmacysRepository,
                                IRepository<Drug> drugsRepository,
                                IRepository<Product> productRepository,
                                IRepository<PurchaseDetail> purchaseDetailRepository,
                                IRepository<PurchaseProductDetail> purchaseProductDetailRepository,
                                IRepository<Session> sessionRespository,
                                IRepository<User> userRespository)
        {
            _purchasesRepository = purchasesRepository;
            _pharmacysRepository = pharmacysRepository;
            _drugsRepository = drugsRepository;
            _productRepository = productRepository;
            _purchaseDetailRepository = purchaseDetailRepository;
            _purchaseProductDetailRepository = purchaseProductDetailRepository;
            _sessionRepository = sessionRespository;
            _userRepository = userRespository;
        }

        public Purchase CreatePurchase(Purchase purchase)
        {
            string validEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Regex rgEmail = new(validEmail);
            if (string.IsNullOrEmpty(purchase.BuyerEmail) || !rgEmail.IsMatch(purchase.BuyerEmail))
                throw new InvalidResourceException("Invalid Email");

            /*
             * * This would make buying products or medicines by themselves impossible
            if ((purchase.details == null || purchase.details.Count == 0))
                throw new InvalidResourceException("The list of items can't be empty");
            if ((purchase.ProductDetails == null || purchase.ProductDetails.Count == 0))
                throw new InvalidResourceException("The list of products can't be empty");
            */
            if (purchase.details == null || purchase.details.Count == 0)
            {
                if (purchase.ProductDetails == null || purchase.ProductDetails.Count == 0)
                {
                    throw new InvalidResourceException("The list of items can't be empty");
                }
            }

            if (purchase.PurchaseDate == DateTime.MinValue)
                throw new InvalidResourceException("The purchase date is a mandatory field");

            decimal total = 0;
            if (purchase.details != null)
            {
                foreach (var detail in purchase.details)
                {
                    int pharmacyId = detail.Pharmacy.Id;
                    if (pharmacyId <= 0)
                        throw new ResourceNotFoundException($"Pharmacy Id is a mandatory field");

                    var pharmacy = _pharmacysRepository.GetOneByExpression(x => x.Id == pharmacyId);
                    if (pharmacy is null)
                        throw new ResourceNotFoundException($"Pharmacy {detail.Pharmacy.Id} not found");

                    if (detail.Quantity <= 0)
                        throw new InvalidResourceException("The Quantity is a mandatory field");

                    string drugCode = detail.Drug.Code;
                    var drug = pharmacy.Drugs.FirstOrDefault(x => x.Code == drugCode && x.Deleted == false);
                    if (drug is null)
                        throw new ResourceNotFoundException($"Drug {drugCode} not found in Pharmacy {pharmacy.Name}");

                    detail.Pharmacy = pharmacy;
                    total = total + (drug.Price * detail.Quantity);
                    detail.Price = drug.Price;
                    detail.Drug = drug;
                    detail.Status = PENDING;
                }
            }
            if (purchase.ProductDetails != null)
            {
                foreach (var productDetail in purchase.ProductDetails)
                {
                    int pharmacyId = productDetail.Pharmacy.Id;
                    if (pharmacyId <= 0)
                        throw new ResourceNotFoundException($"Pharmacy Id is a mandatory field");

                    var pharmacy = _pharmacysRepository.GetOneByExpression(x => x.Id == pharmacyId);
                    if (pharmacy is null)
                        throw new ResourceNotFoundException($"Pharmacy {productDetail.Pharmacy.Id} not found");

                    if (productDetail.Quantity <= 0)
                        throw new InvalidResourceException("The Quantity is a mandatory field");

                    string productCode = productDetail.Product.Code;
                    var product = _productRepository.GetOneByExpression(x => x.Code == productCode && x.Pharmacy.Id == pharmacyId && !x.Deleted);

                    if (product is null)
                        throw new ResourceNotFoundException($"Product {productCode} not found in Pharmacy {pharmacy.Name}");

                    productDetail.Pharmacy = pharmacy;
                    total = total + (productDetail.Price * productDetail.Quantity);
                    productDetail.Price = product.Price;
                    productDetail.Product = product;
                    productDetail.Status = PENDING;
                }
            }
            purchase.TotalAmount = total;
            purchase.TrackingCode = generateTrackingCode();
            _purchasesRepository.InsertOne(purchase);
            _purchasesRepository.Save();

            return purchase;
        }

        private string generateTrackingCode()
        {
            Random rand = new Random();
            string charbase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(0, 16).Select(_ => charbase[rand.Next(charbase.Length)]).ToArray());
        }

        public PurchaseDetail ApprovePurchaseDetail(int purchaseId, int pharmacyId, string drugCode)
        {
            Purchase purchase = _purchasesRepository.GetOneDetailByExpression(p => p.Id == purchaseId);
            if (purchase is null)
                throw new ResourceNotFoundException($"Purchase not found {purchaseId}");

            PurchaseDetail purchaseDetail = null;
            foreach (PurchaseDetail d in purchase.details)
            {
                if (d.Pharmacy.Id == pharmacyId && d.Drug.Code == drugCode && d.Status.Equals(PENDING))
                {
                    purchaseDetail = d;
                    break;
                }
            }

            if (purchaseDetail is null)
                throw new ResourceNotFoundException($"Purchase Detail not found for Pharmacy {pharmacyId} and Drug {drugCode}");

            Pharmacy pharmacy = _pharmacysRepository.GetOneByExpression(p => p.Id == pharmacyId);
            if (pharmacy is null)
                throw new ResourceNotFoundException($"Pharmacy with Id: {pharmacyId} not found");

            var drug = pharmacy.Drugs.FirstOrDefault(x => x.Code == drugCode && x.Deleted == false);
            if (drug is null)
                throw new ResourceNotFoundException($"Drug {drugCode} not found in Pharmacy {pharmacy.Name}");

            // Check Stock
            if (purchaseDetail.Quantity > drug.Stock)
                throw new InvalidResourceException($"The Drug {drug.Code} is out of stock in Pharmacy {pharmacy.Name}");

            // Update Stock
            drug.Stock = drug.Stock - purchaseDetail.Quantity;
            _drugsRepository.UpdateOne(drug);
            _drugsRepository.Save();

            purchaseDetail.Status = APPROVED;
            _purchaseDetailRepository.UpdateOne(purchaseDetail);
            _purchaseDetailRepository.Save();

            return purchaseDetail;
        }

        public PurchaseProductDetail ApproveProductPurchaseDetail(int purchaseId, int pharmacyId, string productCode)
        {
            Purchase purchase = _purchasesRepository.GetOneDetailByExpression(p => p.Id == purchaseId);
            if (purchase is null)
                throw new ResourceNotFoundException($"Purchase not found {purchaseId}");

            PurchaseProductDetail purchaseProductDetail = null;
            foreach (PurchaseProductDetail d in purchase.ProductDetails)
            {
                if (d.Pharmacy.Id == pharmacyId && d.Product.Code == productCode && d.Status.Equals(PENDING))
                {
                    purchaseProductDetail = d;
                    break;
                }
            }

            if (purchaseProductDetail is null)
                throw new ResourceNotFoundException($"Purchase Detail not found for Pharmacy {pharmacyId} and Product {productCode}");

            Pharmacy pharmacy = _pharmacysRepository.GetOneByExpression(p => p.Id == pharmacyId);
            if (pharmacy is null)
                throw new ResourceNotFoundException($"Pharmacy with Id: {pharmacyId} not found");

            //I removed a bit of code regarding farmacy checking here too

            purchaseProductDetail.Status = APPROVED;
            _purchaseProductDetailRepository.UpdateOne(purchaseProductDetail);
            _purchaseProductDetailRepository.Save();

            return purchaseProductDetail;
        }

        public PurchaseDetail RejectPurchaseDetail(int purchaseId, int pharmacyId, string drugCode)
        {
            Purchase purchase = _purchasesRepository.GetOneDetailByExpression(p => p.Id == purchaseId);
            if (purchase is null)
                throw new ResourceNotFoundException($"Purchase not found {purchaseId}");
            
            PurchaseDetail purchaseDetail = null;
            foreach (PurchaseDetail d in purchase.details) {
                if (d.Pharmacy.Id == pharmacyId && d.Drug.Code == drugCode && d.Status.Equals(PENDING)) {
                    purchaseDetail = d;
                    break;
                }
            }

            if (purchaseDetail is null)
                throw new ResourceNotFoundException($"Purchase Detail not found for Pharmacy {pharmacyId} and Drug {drugCode}");

            Pharmacy pharmacy = _pharmacysRepository.GetOneByExpression(p => p.Id == pharmacyId);
            if (pharmacy is null)
                throw new ResourceNotFoundException($"Pharmacy with Id: {pharmacyId} not found");

            var drug = pharmacy.Drugs.FirstOrDefault(x => x.Code == drugCode && x.Deleted == false);
            if (drug is null)
                throw new ResourceNotFoundException($"Drug {drugCode} not found in Pharmacy {pharmacy.Name}");

            purchaseDetail.Status = REJECTED;
            _purchaseDetailRepository.UpdateOne(purchaseDetail);
            _purchaseDetailRepository.Save();

            purchase.TotalAmount = purchase.TotalAmount - (purchaseDetail.Price * purchaseDetail.Quantity);

            _purchasesRepository.UpdateOne(purchase);
            _purchasesRepository.Save();

            return purchaseDetail;
        }

        public PurchaseProductDetail RejectPurchaseProductDetail(int purchaseId, int pharmacyId, string productCode)
        {
            Purchase purchase = _purchasesRepository.GetOneDetailByExpression(p => p.Id == purchaseId);
            if (purchase is null)
                throw new ResourceNotFoundException($"Purchase not found {purchaseId}");

            PurchaseProductDetail purchaseProduct = null;
            foreach (PurchaseProductDetail d in purchase.ProductDetails)
            {
                if (d.Pharmacy.Id == pharmacyId && d.Product.Code == productCode && d.Status.Equals(PENDING))
                {
                    purchaseProduct = d;
                    break;
                }
            }

            if (purchaseProduct is null)
                throw new ResourceNotFoundException($"Purchase Detail not found for Pharmacy {pharmacyId} and Drug {productCode}");

            Pharmacy pharmacy = _pharmacysRepository.GetOneByExpression(p => p.Id == pharmacyId);
            if (pharmacy is null)
                throw new ResourceNotFoundException($"Pharmacy with Id: {pharmacyId} not found");

            purchaseProduct.Status = REJECTED;
            _purchaseProductDetailRepository.UpdateOne(purchaseProduct);
            _purchaseProductDetailRepository.Save();

            purchase.TotalAmount = purchase.TotalAmount - (purchaseProduct.Price * purchaseProduct.Quantity);

            _purchasesRepository.UpdateOne(purchase);
            _purchasesRepository.Save();

            return purchaseProduct;
        }

        public ICollection<Purchase> GetAllPurchases(string token)
        {
            var guidToken = new Guid(token);
            Session session = _sessionRepository.GetOneByExpression(s => s.Token == guidToken);
            var userId = session.UserId;
            User user = _userRepository.GetOneDetailByExpression(u => u.Id == userId);
            Pharmacy pharmacy = user.Pharmacy;

            var purchases = _purchasesRepository.GetAllByExpression(s => s.Id > 0);

            ICollection<Purchase> response = new List<Purchase>();
            foreach(Purchase purchase in purchases)
            {
                ICollection<PurchaseDetail> _details = new List<PurchaseDetail>();
                decimal total = 0;
                foreach (PurchaseDetail detail in purchase.details) {
                    if (detail.Pharmacy.Id == pharmacy.Id &&
                        (detail.Status.Equals(PENDING) || detail.Status.Equals(APPROVED))) {
                        total += (detail.Price * detail.Quantity);
                        _details.Add(detail);
                    }
                }
                purchase.details = _details;
                purchase.TotalAmount = total;
                if (_details.Count > 0)
                {
                    response.Add(purchase);
                }
            }
            return response;
        }

        public ICollection<Purchase> GetAllPurchasesByDate(string token, DateTime? start, DateTime? end)
        {
            var guidToken = new Guid(token);
            Session session = _sessionRepository.GetOneByExpression(s => s.Token == guidToken);
            var userId = session.UserId;
            User user = _userRepository.GetOneDetailByExpression(u => u.Id == userId);
            Pharmacy pharmacy = user.Pharmacy;

            if (start is null && end is null)
            {
                var today = DateTime.Now;
                var lastDayOfMonth = DateTime.DaysInMonth(today.Year, today.Month);
                var lastDateOfMonth = new DateTime(today.Year, today.Month, lastDayOfMonth, 23, 59, 59);
                var firstDateOfMonth = new DateTime(today.Year, today.Month, 1, 0, 0, 0);

                IEnumerable<Purchase> list = _purchasesRepository.GetAllByExpression(p =>
                    p.PurchaseDate <= lastDateOfMonth && p.PurchaseDate >= firstDateOfMonth).ToList();

                return GetPurchases(pharmacy, list);

            }
            else if (start is null)
            {
                throw new InvalidResourceException($"Start date is required");
            }
            else if (end is null)
            {
                throw new InvalidResourceException($"End date is required");
            }
            else
            {
                if (start > end)
                    throw new InvalidResourceException($"Start date can't be bigger than End date");

                start = new DateTime(start.Value.Year, start.Value.Month, start.Value.Day, 00, 00, 00);
                end = new DateTime(end.Value.Year, end.Value.Month, end.Value.Day, 23, 59, 59);

                Expression<Func<Purchase, bool>> purchaseFilter = (purchase =>
                     purchase.PurchaseDate <= end && purchase.PurchaseDate >= start);
                IEnumerable<Purchase> list = _purchasesRepository.GetAllByExpression(purchaseFilter).ToList();

                return GetPurchases(pharmacy, list);
            }
        }

        private ICollection<Purchase> GetPurchases(Pharmacy pharmacy, IEnumerable<Purchase> purchases)
        {
            ICollection<Purchase> response = new List<Purchase>();
            foreach (Purchase purchase in purchases)
            {
                ICollection<PurchaseDetail> _details = new List<PurchaseDetail>();
                decimal total = 0;
                foreach (PurchaseDetail detail in purchase.details)
                {
                    if (detail.Pharmacy.Id == pharmacy.Id) {
                        if (detail.Status.Equals(PENDING) || detail.Status.Equals(APPROVED))
                        {
                            total += (detail.Price * detail.Quantity);
                            _details.Add(detail);
                        }
                        else 
                        {
                            _details.Add(detail);
                        }
                    }
                }
                purchase.details = _details;
                purchase.TotalAmount = total;
                if (_details.Count > 0)
                {
                    response.Add(purchase);
                }
            }
            return response;
        }

        public Purchase GetPurchaseByTrackingCode(string trackingCode)
        {
            if (string.IsNullOrEmpty(trackingCode))
                throw new InvalidResourceException($"Tracking Code is can't be empty");

            return _purchasesRepository.GetOneDetailByExpression(p => p.TrackingCode == trackingCode);
        }
    }
}