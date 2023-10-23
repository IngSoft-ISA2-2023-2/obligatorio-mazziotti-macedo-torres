using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using Microsoft.EntityFrameworkCore;
using PharmaGo.BusinessLogic;
using PharmaGo.DataAccess;
using PharmaGo.DataAccess.Repositories;
using PharmaGo.Domain.Entities;
using PharmaGo.IBusinessLogic;
using PharmaGo.IDataAccess;
using PharmaGo.WebApi.Controllers;
using PharmaGo.WebApi.Converters;
using PharmaGo.WebApi.Models.In;
using PharmaGo.WebApi.Models.Out;
using System;
using TechTalk.SpecFlow;
using static PharmaGo.WebApi.Models.In.PurchaseModelRequest;

namespace SpecFlow.Specs.StepDefinitions
{
    [Binding]
    public class PurchaseProductsInTheShoppingCartStepDefinitions
    {
        private PharmacyGoDbContext _dbContext;
        private IRepository<Product> _productRepository;
        private IRepository<Drug> _drugRepository;
        private IRepository<Pharmacy> _pharmacyRepository;
        private IRepository<Session> _sessionRepository;
        private IRepository<User> _userRepository;
        private IRepository<Purchase> _purchaseRepository;
        private IRepository<PurchaseDetail> _purchaseDetailRepository;
        private IRepository<PurchaseProductDetail> _purchaseProductDetailRepository;
        private IPurchasesManager _purchasesManager;
        private PurchasesController _purchaseController;
        private PurchaseProductDetailModelRequest _purchaseProductDetail;
        private PurchaseModelRequest _purchaseModel;
        private string _responseError;
        private IActionResult _response;

        [BeforeScenario]
        public void Setup()
        {
            var connectionString = "Server=localhost\\SQLEXPRESS;Database=PharmaGoDb;Trusted_Connection=True; MultipleActiveResultSets=True";
            var optionsBuilder = new DbContextOptionsBuilder<PharmacyGoDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            _dbContext = new PharmacyGoDbContext(optionsBuilder.Options);

            _productRepository = new ProductRepository(_dbContext);
            _drugRepository = new DrugRepository(_dbContext);
            _pharmacyRepository = new PharmacyRepository(_dbContext);
            _sessionRepository = new SessionRepository(_dbContext);
            _userRepository = new UsersRepository(_dbContext);
            _purchaseRepository = new PurchasesRepository(_dbContext);
            _purchaseDetailRepository = new PurchasesDetailRepository(_dbContext);

            //This line is horrible but we're adhering to the original implementation
            _purchasesManager = new PurchasesManager(_purchaseRepository, _pharmacyRepository, _drugRepository, _productRepository, _purchaseDetailRepository, _purchaseProductDetailRepository,_sessionRepository, _userRepository);

            _purchaseController = new PurchasesController(_purchasesManager);
            /*
            _productManager = new ProductManager(_productRepository, _pharmacyRepository, _sessionRepository, _userRepository);
            _productController = new ProductController(_productManager);
            */
        }

        [Given(@"I am a customer on the platform")]
        public void GivenIAmACustomerOnThePlatform()
        {
            //Empty step
        }

        [Given(@"I have one or more products in my shopping cart")]
        public void GivenIHaveOneOrMoreProductsInMyShoppingCart()
        {
            //This product needs to already exist in the database
            _purchaseProductDetail = new PurchaseProductDetailModelRequest()
            {
                PharmacyId = 1,
                Code = "66666",
                Quantity = 1
            };
            _purchaseModel = new PurchaseModelRequest()
            {
                BuyerEmail = "testTest@test.com",
                PurchaseDate = DateTime.Now,
                Details = new List<PurchaseDetailModelRequest>(),
                ProductDetails = new List<PurchaseProductDetailModelRequest>()
            };

            _purchaseModel.ProductDetails.Add(_purchaseProductDetail);
        }

        [When(@"I proceed to checkout and confirm the purchase")]
        public void WhenIProceedToCheckoutAndConfirmThePurchase()
        {
            try
            {
                _response = _purchaseController.CreatePurchase(_purchaseModel);
            }
            catch (Exception ex)
            {
                _responseError = ex.Message;
            }
        }

        [Then(@"the system should create an order for the selected products")]
        public void ThenTheSystemShouldCreateAnOrderForTheSelectedProducts()
        {
            List<Purchase> purchases = _purchaseRepository.GetAllByExpression(p => true).ToList();
            
            var converter = new PurchaseModelRequestToPurchaseConverter();
            var purchase = converter.Convert(_purchaseModel);

            //Esto me va a dar mal creo porque el objeto no es estrictamente el mismo
            Assert.NotEmpty(purchases.FindAll(x => x.BuyerEmail.Equals(purchase.BuyerEmail)));
        }

        [Then(@"the system should provide an order confirmation")]
        public void ThenTheSystemShouldProvideAnOrderConfirmationWithAUniqueOrderNumber()
        {
            var result = _response as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode.Value);
        }

        [Given(@"I have no products in my shopping cart")]
        public void GivenIHaveNoProductsInMyShoppingCart()
        {
            _purchaseModel = new PurchaseModelRequest()
            {
                BuyerEmail = "a@a.com",
                PurchaseDate = DateTime.Now,
                Details = new List<PurchaseDetailModelRequest>() {}
            };
        }

        [Then(@"the system should display an error message indicating that the cart is empty")]
        public void ThenTheSystemShouldDisplayAnErrorMessageIndicatingThatTheCartIsEmpty()
        {
            Assert.Equal("The list of items can't be empty", _responseError);
        }

        [Then(@"I should not be able to complete the purchase")]
        public void ThenIShouldNotBeAbleToCompleteThePurchase()
        {
            //Empty step
        }
    }
}
