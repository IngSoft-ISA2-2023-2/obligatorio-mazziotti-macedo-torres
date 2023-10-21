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

        [BeforeScenario]
        public void Setup()
        {
            var connectionString = "Server=LAPTOP-KE22VQHH;Database=PharmaGoDb;Trusted_Connection=True; MultipleActiveResultSets=True";
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

            if (_productRepository.GetAllByExpression(p => p.Code == "1234A").Count() == 0)
            {
                Product product = new Product()
                {
                    Code = "1234A",
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
            httpContext.Request.Headers["Authorization"] = "Bearer E9E0E1E9-3812-4EB5-949E-AE92AC931401";

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

        [Given(@"the system already has a product with the same code I enter")]
        public void GivenTheSystemAlreadyHasAProductWithTheSameCodeIEnter()
        {
            //throw new PendingStepException();
        }

        [When(@"I attempt to add the product")]
        public void WhenIAttemptToAddTheProduct()
        {
            //throw new PendingStepException();
        }

        [Then(@"the system shows an error message indicating the product code already exists and must be unique")]
        public void ThenTheSystemShowsAnErrorMessageIndicatingTheProductCodeAlreadyExistsAndMustBeUnique()
        {
            //throw new PendingStepException();
        }

        [Given(@"I enter the code '([^']*)' for the product")]
        public void GivenIEnterTheCodeForTheProduct(string p0)
        {
            //throw new PendingStepException();
        }

        [Given(@"I enter the name '([^']*)' for the product")]
        public void GivenIEnterTheNameForTheProduct(string p0)
        {
            //throw new PendingStepException();
        }

        [Given(@"I enter the description '([^']*)' for the product")]
        public void GivenIEnterTheDescriptionForTheProduct(string p0)
        {
            //throw new PendingStepException();
        }

        [Given(@"I enter the price '([^']*)' for the product")]
        public void GivenIEnterThePriceForTheProduct(string p0)
        {
            //throw new PendingStepException();
        }

        [Then(@"the system shows an error message indicating '([^']*)'")]
        public void ThenTheSystemShowsAnErrorMessageIndicating(string p0)
        {
            //throw new PendingStepException();
        }
    }
}
