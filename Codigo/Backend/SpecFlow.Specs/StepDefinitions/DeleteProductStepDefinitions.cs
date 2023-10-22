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

namespace SpecFlow.Specs.StepDefinitions
{
    [Binding]
    public class DeleteProductStepDefinitions
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

            /*
            if (products.Count() == 1)
            {
                _productRepository.DeleteOne(products[0]);
                _productRepository.Save();
            }
            */
        }

        [Given(@"that I am an authorized pharmacy employee")]
        public void GivenThatIAmAnAuthorizedEmployeeOfThePharmacy()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "sE9E0E1E9-3812-4EB5-949E-AE92AC931401";

            _productController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        [Given(@"I select the option to delete a product")]
        public void GivenISelectTheOptionToDeleteAProduct()
        {
            //Empty step
        }

        [Given(@"the system displays a list of existing products")]
        public void GivenTheSystemDisplaysAListOfExistingProducts()
        {
            //Empty step
        }

        [When(@"I delete the product")]
        public void WhenISelectTheProductIWantToDelete()
        {
            _response = _productController.Delete(_productModel);
        }

        [Then(@"the system marks the product as ""([^""]*)"" in the database")]
        public void ThenTheSystemMarksTheProductAsInTheDatabase(string unavailable)
        {
            List<Product> products = _productRepository.GetAllByExpression(p => p.Code == "12345").ToList();

            Assert.Single(products);
            Assert.True(products[0].Deleted);
        }

        [Then(@"displays a confirmation message indicating that the product has been successfully deleted")]
        public void ThenDisplaysAConfirmationMessageIndicatingThatTheProductHasBeenSuccessfullyDeleted()
        {
            var result = _response as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode.Value);
        }

        [When(@"I attempt to delete a product that does not exist in the system")]
        public void WhenIAttemptToDeleteAProductThatDoesNotExistInTheSystem()
        {
            var _nonexistantProduct = new ProductModel();

            _response = _productController.Delete(_nonexistantProduct);
        }

        [Then(@"the system informs me that the product does not exist")]
        public void ThenTheSystemInformsMeThatTheProductDoesNotExist()
        {
            var result = _response as ObjectResult;
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode.Value);
        }

        [Then(@"takes no action")]
        public void ThenTakesNoAction()
        {
            //Empty step
        }

        [Given(@"I am in the process of deactivating a product")]
        public void GivenIAmInTheProcessOfDeactivatingAProduct()
        {
            //Empty step
        }

        [When(@"I cancel the operation")]
        public void WhenICancelTheOperation()
        {
            //Empty step
        }

        [Then(@"the system makes no modifications to the database")]
        public void ThenTheSystemMakesNoModificationsToTheDatabase()
        {
            //Empty step
        }
    }
}
