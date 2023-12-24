using Microsoft.AspNetCore.Mvc;
using fin_manager.Models;
using fin_manager.Services.Interfaces;
using fin_manager.Services;
using fin_manager.Utils.Enum;
using fin_manager.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fin_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPurchaseService _purchaseService;

        public UserController(IUserService userService, IPurchaseService purchaseService)
        {
            _userService = userService;
            _purchaseService = purchaseService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<UserModel>> GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                if (users == null) throw new Exception("Error finding users.");

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUserById(string id)
        {
            try
            {
                var userExists = _userService.GetUserById(id);
                if (userExists == null) throw new Exception("User not registered.");

                return Ok(userExists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}/purchases")]
        public ActionResult<UserModel> GetUserPurchasesById(string id)
        {
            try
            {
                var userExists = _userService.GetUserById(id);
                if (userExists == null) throw new Exception("User not registered.");

                var userPurchases = _purchaseService.GetUserPurchases(userExists.Purchases).ToList();

                return Ok(userPurchases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public  ActionResult<UserModel> CreateUser([FromBody] UserModel user)
        {
            try
            {
                var userExists = _userService.GetUserByEmail(user.Email);
                if (userExists != null) throw new Exception("User already exists.");

                var userCreated = _userService.CreateUser(user);
                if (userCreated == null) throw new Exception("User not created.");

                return Ok(userCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/purchases")]
        public ActionResult AddPurchaseToUser(string id, [FromBody] PurchaseModel purchase)
        {
            try
            {
                UserModel userExists = _userService.GetUserById(id);
                if (userExists == null) throw new Exception("User not found.");

                PurchaseModel purchaseCreated = _purchaseService.CreatePurchase(purchase);
                if (purchaseCreated == null) throw new Exception("Purchase not creadted.");

                userExists.AddPurchaseToUser(purchaseCreated.Id);

                _userService.UpdateUser(id, userExists);

                return Ok(userExists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult UpdateUser(string id, [FromBody] UserModel user)
        {
            try
            {
                var userExists = _userService.GetUserById(id);
                if (userExists == null) return NotFound("User not found.");

                _userService.UpdateUser(id, user);

                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var userExists = _userService.GetUserById(id);
                if (userExists == null) return NotFound("User not found.");

                var userPurchases = _purchaseService.GetUserPurchases(userExists.Purchases).ToList();
                foreach(var purchase in userPurchases)
                {
                    _purchaseService.DeletePurchase(purchase.Id);
                }

                _userService.DeleteUser(userExists.Id);

                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
