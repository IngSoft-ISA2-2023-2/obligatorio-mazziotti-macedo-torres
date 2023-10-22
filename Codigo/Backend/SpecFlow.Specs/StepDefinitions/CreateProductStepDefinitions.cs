using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaGo.BusinessLogic;
using PharmaGo.DataAccess;
using PharmaGo.DataAccess.Repositories;
using PharmaGo.Domain.Entities;
using PharmaGo.IBusinessLogic;
using PharmaGo.IDataAccess;
using PharmaGo.WebApi.Controllers;
using PharmaGo.WebApi.Models.In;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;

namespace SpecFlow.Specs.StepDefinitions
{
    [Binding]
    public class CreateProductStepDefinitions
    {
        private PharmacyGoDbContext _dbContext;
        private IRepository<Product> _productRepository;
        private IRepository<Pharmacy> _pharmacyRepository;
        private IRepository<Session> _sessionRepository;
        private IRepository<User> _userRepository;
        private ProductModel _productModel;
        private ProductManager _productManager;
        private ProductController _productController;
        private IActionResult _response;
        private string _responseError;

        [BeforeScenario]
        public void Setup()
        {
            var connectionString = "Server=localhost\\SQLEXPRESS;Database=PharmaGoDb;Trusted_Connection=True; MultipleActiveResultSets=True";
            var optionsBuilder = new DbContextOptionsBuilder<PharmacyGoDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            _dbContext = new PharmacyGoDbContext(optionsBuilder.Options);

            _productRepository = new ProductRepository(_dbContext);
            _pharmacyRepository = new PharmacyRepository(_dbContext);
            _sessionRepository = new SessionRepository(_dbContext);
            _userRepository = new UsersRepository(_dbContext);

            _productManager = new ProductManager(_productRepository, _pharmacyRepository, _sessionRepository, _userRepository);

            _productController = new ProductController(_productManager);

            _productModel = new ProductModel();

            if (_productRepository.GetAllByExpression(p => p.Code == "54321").Count() == 0)
            {
                Product product = new Product()
                {
                    Code = "54321",
                    Name = "Name",
                    Description = "Description",
                    Price = 10.5M,
                    Pharmacy = _pharmacyRepository.GetOneByExpression(f => f.Id == 1)
                };

                _productRepository.InsertOne(product);
                _productRepository.Save();
            }

            List<Product> products = _productRepository.GetAllByExpression(p => p.Code == "12345").ToList();

            if (products.Count() == 1)
            {
                _productRepository.DeleteOne(products[0]);
                _productRepository.Save();
            }
        }

        [Given(@"I am an authorized pharmacy employee")]
        public void GivenIAmAnAuthorizedPharmacyEmployee()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "E9E0E1E9-3812-4EB5-949E-AE92AC931401";

            _productController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        [Given(@"I enter a valid code for a new product")]
        public void GivenIEnterAValidCodeForANewProduct()
        {
            _productModel.Code = "12345";
        }

        [Given(@"I enter a valid name")]
        public void GivenIEnterAValidName()
        {
            _productModel.Name = "Valid Name";
        }

        [Given(@"I enter a valid description")]
        public void GivenIEnterAValidDescription()
        {
            _productModel.Description = "Valid Description";
        }

        [Given(@"I enter a valid price")]
        public void GivenIEnterAValidPrice()
        {
            _productModel.Price = 10.5M;
        }

        [When(@"I request its addition")]
        public void WhenIRequestItsAddition()
        {
            _response = _productController.Create(_productModel);
        }

        [Then(@"the system validates that all data is present and correct")]
        public void ThenTheSystemValidatesThatAllDataIsPresentAndCorrect()
        {
            var result = _response as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode.Value);
        }

        [Then(@"the system inserts the new product into the product database")]
        public void ThenTheSystemInsertsTheNewProductIntoTheProductDatabase()
        {
            IEnumerable<Product> products = _productRepository.GetAllByExpression(p => p.Code == "12345");

            Assert.Single(products);
        }

        [Then(@"the system displays a confirmation message indicating the product was successfully added to the catalog")]
        public void ThenTheSystemDisplaysAConfirmationMessageIndicatingTheProductWasSuccessfullyAddedToTheCatalog()
        {
            var result = _response as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode.Value);
        }

        [When(@"I attempt to add the product")]
        public void WhenIAttemptToAddTheProduct()
        {
            try
            {
                _response = _productController.Create(_productModel);
            }
            catch (Exception e)
            {
                _responseError = e.Message;
            }
        }

        [Given(@"I enter the code '([^']*)' for the product")]
        public void GivenIEnterTheCodeForTheProduct(string code)
        {
            _productModel.Code = code;
        }

        [Given(@"I enter the name '([^']*)' for the product")]
        public void GivenIEnterTheNameForTheProduct(string name)
        {
            _productModel.Name = name;
        }

        [Given(@"I enter the description '([^']*)' for the product")]
        public void GivenIEnterTheDescriptionForTheProduct(string description)
        {
            _productModel.Description = description;
        }

        [Given(@"I enter the price '([^']*)' for the product")]
        public void GivenIEnterThePriceForTheProduct(string price)
        {
            _productModel.Price = decimal.Parse(price);
        }

        [Then(@"the system shows an error message indicating '([^']*)'")]
        public void ThenTheSystemShowsAnErrorMessageIndicating(string errorMessage)
        {
            Assert.Equal(errorMessage, _responseError);
        }
    }
}
