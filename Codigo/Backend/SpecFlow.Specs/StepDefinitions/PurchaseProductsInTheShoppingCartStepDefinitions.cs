using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
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
        private PurchasesController _purchaseController;
        private PurchasesRepository _purchaseRepository;
        private PurchasesDetailRepository _purchaseDetailRepository;
        private PurchaseDetailModelRequest _purchaseDetail;
        private PurchaseModelRequest _purchaseModel;
        private string _responseError;

        private IActionResult _response;

        [Given(@"I am a customer on the platform")]
        public void GivenIAmACustomerOnThePlatform()
        {
            //Empty step
        }

        [Given(@"I have one or more products in my shopping cart")]
        public void GivenIHaveOneOrMoreProductsInMyShoppingCart()
        {
            //This product needs to already exist in the database
            _purchaseDetail = new PurchaseDetailModelRequest()
            {
                PharmacyId = 1,
                Code = "66666",
                Quantity = 1
            };
            _purchaseModel = new PurchaseModelRequest()
            {
                BuyerEmail = "a@a.com",
                PurchaseDate = DateTime.Now,
                Details = new List<PurchaseDetailModelRequest>() {_purchaseDetail}
            };
        }

        [When(@"I proceed to checkout and confirm the purchase")]
        public void WhenIProceedToCheckoutAndConfirmThePurchase()
        {
            _response = _purchaseController.CreatePurchase(_purchaseModel);
        }

        [Then(@"the system should create an order for the selected products")]
        public void ThenTheSystemShouldCreateAnOrderForTheSelectedProducts()
        {
            List<Purchase> purchases = _purchaseRepository.GetAllByExpression(p => true).ToList();
            
            var converter = new PurchaseModelRequestToPurchaseConverter();
            var purchase = converter.Convert(_purchaseModel);

            //Esto me va a dar mal creo porque el objeto no es estrictamente el mismo
            Assert.Contains(purchase, purchases);
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
            try
            {
                _response = _purchaseController.CreatePurchase(_purchaseModel);
            }
            catch (Exception ex)
            {
                _responseError = ex.Message;
            }
        }

        [Then(@"I should not be able to complete the purchase")]
        public void ThenIShouldNotBeAbleToCompleteThePurchase()
        {
            Assert.Equal("Cart is empty", _responseError);
        }
    }
}
