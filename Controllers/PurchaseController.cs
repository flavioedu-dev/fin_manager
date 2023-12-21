using fin_manager.Models;
using fin_manager.Models.Interfaces;
using fin_manager.Services.Interfaces;
using fin_manager.Utils;
using fin_manager.Utils.Enum;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fin_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ErrorHelper _errorHelper = new ErrorHelper();

        public PurchaseController(IPurchaseService purchaseService, IProductService productService, IUserService userService)
        {
            _purchaseService = purchaseService;
            _productService = productService;
            _userService = userService;
        }

        // GET: api/<PurchaseController>
        [HttpGet]
        public ActionResult<List<PurchaseModel>> GetPurchases()
        {
            try
            {
                List<PurchaseModel> purchases = _purchaseService.GetPurchases();
                if (purchases == null || purchases.Count == 0) throw new Exception(_errorHelper.GetErrorMsg(ApiError.NoneWereFound, "Purchase"));

                return Ok(purchases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PurchaseController>/5
        [HttpGet("{id}")]
        public ActionResult<PurchaseModel> GetPurchaseById(string id)
        {
            try
            {
                PurchaseModel purchaseExists = _purchaseService.GetPurchaseById(id);
                if (purchaseExists == null) throw new Exception(_errorHelper.GetErrorMsg(ApiError.OneNotFound, "Purchase"));

                List<ProductModel> purchaseProducts = _productService.GetPurchaseProducts(purchaseExists.Products);

                purchaseExists.TotalPurchasePrice(purchaseProducts);

                return Ok(purchaseExists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/products")]
        public ActionResult<List<ProductModel>> GetPurchaseProductsById(string id)
        {
            try
            {
                PurchaseModel purchaseExists = _purchaseService.GetPurchaseById(id);
                if (purchaseExists == null) throw new Exception(_errorHelper.GetErrorMsg(ApiError.OneNotFound, "Purchase"));

                List<ProductModel> purchaseProducts = _productService.GetPurchaseProducts(purchaseExists.Products);

                return Ok(purchaseProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PurchaseController>
        [HttpPost]
        public ActionResult CreatePurchase([FromBody] PurchaseModel purchase)
        {
            try
            {
                PurchaseModel purchaseCreated = _purchaseService.CreatePurchase(purchase);
                if (purchaseCreated == null) throw new Exception(_errorHelper.GetErrorMsg(ApiError.NotCreated, "Purchase"));

                return Ok(purchaseCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PurchaseController>/5
        [HttpPut("{id}")]
        public ActionResult UpdatePurchase(string id, [FromBody] PurchaseModel purchase)
        {
            try
            {
                PurchaseModel purchaseExists = _purchaseService.GetPurchaseById(id);
                if (purchaseExists == null) throw new Exception(_errorHelper.GetErrorMsg(ApiError.NotUptaded, "Purchase"));

                _purchaseService.UpdatePurchase(id, purchase);

                return Ok("Purchase updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/products")]
        public ActionResult AddProductToPurchase(string id, [FromBody] ProductModel product)
        {
            try
            {
                PurchaseModel purchaseExists = _purchaseService.GetPurchaseById(id);
                if (purchaseExists == null) throw new Exception(_errorHelper.GetErrorMsg(ApiError.OneNotFound, "Purchase"));

                ProductModel productCreated = _productService.CreateProduct(product);
                if (purchaseExists == null) throw new Exception(_errorHelper.GetErrorMsg(ApiError.NotCreated, "Product"));

                purchaseExists.AddProduct(productCreated.Id);

                _purchaseService.UpdatePurchase(id, purchaseExists);

                return Ok(purchaseExists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PurchaseController>/5
        [HttpDelete("{id}")]
        public ActionResult DeletePurchase(string id)
        {
            try
            {
                PurchaseModel purchaseExists = _purchaseService.GetPurchaseById(id);
                if (purchaseExists == null) throw new Exception(_errorHelper.GetErrorMsg(ApiError.NotDeleted, "Purchase"));

                _purchaseService.DeletePurchase(id);

                return Ok("Purchase deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
