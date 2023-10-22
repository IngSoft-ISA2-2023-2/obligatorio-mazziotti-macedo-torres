using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaGo.BusinessLogic;
using PharmaGo.DataAccess;
using PharmaGo.DataAccess.Repositories;
using PharmaGo.Domain.Entities;
using PharmaGo.IDataAccess;
using PharmaGo.WebApi.Controllers;
using PharmaGo.WebApi.Models.In;
using System;
using TechTalk.SpecFlow;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SpecFlow.Specs.StepDefinitions
{
    [Binding]
    public class ModifyProductInformationStepDefinitions
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
        private int _productId;

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

            if (_productRepository.GetAllByExpression(p => p.Code == "12345").Count() == 0)
            {
                Product product = new Product()
                {
                    Code = "12345",
                    Name = "Name",
                    Description = "Description",
                    Price = 10.5M,
                    Pharmacy = _pharmacyRepository.GetOneByExpression(f => f.Id == 1)
                };

                _productRepository.InsertOne(product);
                _productRepository.Save();
            }

            if (_productRepository.GetAllByExpression(p => p.Code == "44444").Count() == 0)
            {
                Product product = new Product()
                {
                    Code = "44444",
                    Name = "Name",
                    Description = "Description",
                    Price = 10.5M,
                    Pharmacy = _pharmacyRepository.GetOneByExpression(f => f.Id == 1)
                };

                _productRepository.InsertOne(product);
                _productRepository.Save();
            }

            List<Product> products = _productRepository.GetAllByExpression(p => p.Code == "54321").ToList();

            if (products.Count() == 1)
            {
                _productRepository.DeleteOne(products[0]);
                _productRepository.Save();
            }
        }

        [Given(@"I am a pharmacy employee")]
        public void GivenIAmAPharmacyEmployee()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "E9E0E1E9-3812-4EB5-949E-AE92AC931401";

            _productController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        [Given(@"I choose an existing product to modify")]
        public void GivenIChooseAnExistingProductToModify()
        {
            if (_productRepository.GetAllByExpression(p => p.Code == "33333").Count() == 0)
            {
                Product product = new Product()
                {
                    Code = "33333",
                    Name = "Name",
                    Description = "Description",
                    Price = 10.5M,
                    Pharmacy = _pharmacyRepository.GetOneByExpression(f => f.Id == 1)
                };

                _productRepository.InsertOne(product);
                _productRepository.Save();
            }
            
            _productId = _productRepository.GetAllByExpression(p => p.Code == "33333").FirstOrDefault().Id;
        }

        [Given(@"I enter the new valid code")]
        public void GivenIEnterTheNewValidCode()
        {
            _productModel.Code = "89898";
        }

        [Given(@"I enter the new valid name")]
        public void GivenIEnterTheNewValidName()
        {
            _productModel.Name = "New Name";
        }

        [Given(@"I enter the new valid description")]
        public void GivenIEnterTheNewValidDescription()
        {
            _productModel.Description = "New Description";
        }

        [Given(@"I enter the new valid price")]
        public void GivenIEnterTheNewValidPrice()
        {
            _productModel.Price = 20.5M;
        }

        [When(@"I request the product modification")]
        public void WhenIRequestTheProductModification()
        {
            _response = _productController.ModifyProduct(_productId, _productModel);
        }

        [Then(@"the system validates the new information")]
        public void ThenTheSystemValidatesTheNewInformation()
        {
            var result = _response as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode.Value);
        }

        [Then(@"updates the database with the new product details")]
        public void ThenUpdatesTheDatabaseWithTheNewProductDetails()
        {
            Assert.Equal(_productRepository.GetAllByExpression(p => p.Id == _productId).FirstOrDefault().Code, _productModel.Code);
        }

        [Then(@"it displays a confirmation message indicating that the modification has been successfully carried out")]
        public void ThenItDisplaysAConfirmationMessageIndicatingThatTheModificationHasBeenSuccessfullyCarriedOut()
        {
            var result = _response as ObjectResult;
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode.Value);
        }

        [Given(@"I choose a product to modify that does not exist in the database")]
        public void GivenIChooseAProductToModifyThatDoesNotExistInTheDatabase()
        {
            _productId = 1000000;
        }

        [When(@"I choose the option to modify a product")]
        public void WhenIChooseTheOptionToModifyAProduct()
        {
            try
            {
                _response = _productController.ModifyProduct(_productId, _productModel);
            }
            catch (Exception e)
            {
                _responseError = e.Message;
            }
        }

        [Then(@"the system informs me that the product does not exist")]
        public void ThenTheSystemInformsMeThatTheProductDoesNotExist()
        {
            Assert.Equal("The product does not exist.", _responseError);
        }

        [Given(@"I enter the new code '([^']*)'")]
        public void GivenIEnterTheNewCode(string code)
        {
            _productModel.Code = code;
        }

        [Given(@"I enter the new name '([^']*)'")]
        public void GivenIEnterTheNewName(string name)
        {
            _productModel.Name = name;
        }

        [Given(@"I enter the new description '([^']*)'")]
        public void GivenIEnterTheNewDescription(string description)
        {
            _productModel.Description = description;
        }

        [Then(@"the system shows the error message '([^']*)'")]
        public void ThenTheSystemShowsTheErrorMessage(string errorMessage)
        {
            Assert.Equal(errorMessage, _responseError);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_productRepository.GetAllByExpression(p => p.Code == "12345").Count() == 1)
            {
                _productRepository.DeleteOne(_productRepository.GetAllByExpression(p => p.Code == "12345").FirstOrDefault());
                _productRepository.Save();
            }

            if (_productRepository.GetAllByExpression(p => p.Code == "54321").Count() == 1)
            {
                _productRepository.DeleteOne(_productRepository.GetAllByExpression(p => p.Code == "12345").FirstOrDefault());
                _productRepository.Save();
            }

            if (_productRepository.GetAllByExpression(p => p.Code == "33333").Count() == 1)
            {
                _productRepository.DeleteOne(_productRepository.GetAllByExpression(p => p.Code == "33333").FirstOrDefault());
                _productRepository.Save();
            }

            if (_productRepository.GetAllByExpression(p => p.Code == "89898").Count() == 1)
            {
                _productRepository.DeleteOne(_productRepository.GetAllByExpression(p => p.Code == "89898").FirstOrDefault());
                _productRepository.Save();
            }
        }
    }
}
