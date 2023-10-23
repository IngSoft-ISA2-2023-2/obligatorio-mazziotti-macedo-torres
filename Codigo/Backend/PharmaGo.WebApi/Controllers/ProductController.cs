using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaGo.BusinessLogic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using PharmaGo.Domain.Entities;
using PharmaGo.IBusinessLogic;
using PharmaGo.WebApi.Enums;
using PharmaGo.WebApi.Filters;
using PharmaGo.WebApi.Models.In;
using PharmaGo.WebApi.Models.Out;

namespace PharmaGo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionFilter))]
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductController(IProductManager manager)
        {
            _productManager = manager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = _productManager.GetAll();
            IEnumerable<ProductDetailModel> productsToReturn = products.Select(d => new ProductDetailModel(d));
            return Ok(productsToReturn);
        }

        [HttpGet]
        [Route("[action]")]
        [AuthorizationFilter(new string[] { nameof(RoleType.Employee) })]
        public IActionResult User()
        {
            string token = HttpContext.Request.Headers["Authorization"];
            IEnumerable<Product> products = _productManager.GetAllByUser(token);
            IEnumerable<ProductDetailModel> productToReturn = products.Select(p => new ProductDetailModel(p));
            return Ok(productToReturn);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            Product product = _productManager.GetById(id);
            return Ok(new ProductDetailModel(product));
        }

        [HttpPost]
        [AuthorizationFilter(new string[] { nameof(RoleType.Employee) })]
        public IActionResult Create([FromBody] ProductModel productModel)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            Product productCreated = _productManager.Create(productModel.ToEntity(), token);
            ProductDetailModel productResponse = new ProductDetailModel(productCreated);
            return Ok(productResponse);
        }

        [HttpPut("{id}")]
        [AuthorizationFilter(new string[] { nameof(RoleType.Employee) })]
        public IActionResult Modify([FromRoute] int id, [FromBody] ProductModel productModel)
        {
            Product productUpdated = _productManager.Update(id, productModel.ToEntity());
            ProductDetailModel productResponse = new ProductDetailModel(productUpdated);
            return Ok(productResponse);
        }
        
        [HttpDelete("{id}")]
        [AuthorizationFilter(new string[] { nameof(RoleType.Employee) })]
        public IActionResult Delete([FromRoute] int id)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            _productManager.Delete(id);
            return Ok(200);
        }
    }
}
