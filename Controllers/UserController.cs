using Microsoft.AspNetCore.Mvc;
using fin_manager.Models;
using fin_manager.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fin_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<UserModel>> GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                if (users == null || users.Count == 0) throw new Exception("No registered users.");

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
