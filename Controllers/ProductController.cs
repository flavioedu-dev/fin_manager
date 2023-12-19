using Microsoft.AspNetCore.Mvc;
using fin_manager.Models;
using fin_manager.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fin_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<List<ProductModel>> GetProducts ()
        {
            try
            {
                List<ProductModel> products = _productService.GetProducts();
                if (products == null || products.Count == 0) throw new Exception("No registered products.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductModel> GetProductById(string id)
        {
            try
            {
                ProductModel productExists = _productService.GetProductById(id);
                if (productExists == null) throw new Exception("Product not registered.");

                return Ok(productExists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/purchases")]
        public ActionResult<List<ProductModel>> GetManyProducts(PurchaseModel purchase)
        {
            try
            {
                List<ProductModel> purchaseProducts = _productService.GetManyProducts(purchase.Products);
                if (purchaseProducts == null || purchaseProducts.Count == 0) throw new Exception("No registered products.");

                return Ok(purchaseProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public ActionResult<ProductModel> CreateProduct([FromBody] ProductModel product)
        {
            try
            {
                ProductModel productCreated = _productService.CreateProduct(product);

                return Ok(productCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(string id, [FromBody] ProductModel product)
        {
            try
            {
                ProductModel productExists = _productService.GetProductById(id);
                if (productExists == null) return NotFound("Product not found.");

                _productService.UpdateProduct(id, product);

                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(string id)
        {
            try
            {
                ProductModel productExists = _productService.GetProductById(id);
                if (productExists == null) return NotFound("Product not found.");

                _productService.DeleteProduct(id);

                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
