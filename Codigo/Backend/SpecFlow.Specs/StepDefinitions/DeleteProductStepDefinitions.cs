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
using PharmaGo.WebApi.Models.Out;
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
        }

        [Given(@"that I am an authorized pharmacy employee")]
        public void GivenThatIAmAnAuthorizedEmployeeOfThePharmacy()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "E9E0E1E9-3812-4EB5-949E-AE92AC931401";

            _productController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            if (_productRepository.GetAllByExpression(p => p.Code == "66666").Count() == 0)
            {
                var name = _pharmacyRepository.GetOneByExpression(f => f.Id == 1).Name;
                _productModel = new ProductModel()
                {
                    Code = "66666",
                    Name = "Valid Name",
                    Description = "Valid Description",
                    Price = 10.5M,
                    PharmacyName = name
                };
                _productController.Create(_productModel);
            }
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
            //OkObjectResult response = (OkObjectResult)_productController.GetUnfiltered();
            //IEnumerable<ProductDetailModel> products = (IEnumerable<ProductDetailModel>)response.Value;
            //var product = products.FirstOrDefault(p => p.Code.Equals("66666"));

            ProductDetailModel product = new ProductDetailModel(_productRepository.GetAllByExpression(p => p.Code == "66666").First());

            _response = _productController.Delete(product.Id);
        }

        [Then(@"the system marks the product as ""([^""]*)"" in the database")]
        public void ThenTheSystemMarksTheProductAsInTheDatabase(string unavailable)
        {
            List<Product> products = _productRepository.GetAllByExpression(p => p.Code == "66666").ToList();

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
            var nonexistentProductId = 123456789;
            try
            {
                _response = _productController.Delete(nonexistentProductId);
            }
            catch (Exception ex)
            {
                _responseError = ex.Message;
            }
        }

        [Then(@"the system informs me that the product does not exist")]
        public void ThenTheSystemInformsMeThatTheProductDoesNotExist()
        {
            Assert.Equal("The drug to delete does not exist.", _responseError);
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
