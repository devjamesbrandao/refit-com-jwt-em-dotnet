using Microsoft.AspNetCore.Mvc;
using web_teste.Models;
using web_teste.Services;

namespace web_teste.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private List<User> _logins = new List<User>
        {
            new User("Kakashi", "Rin"),
            new User("Naruto", "Hokage")
        };

        private readonly IAutenticationService _autenticationService;

        public LoginController(IAutenticationService autenticationService)
        {
            _autenticationService = autenticationService;
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] User login)
        {
            var user = _logins.SingleOrDefault(
                p => string.Equals(login.Name, p.Name, StringComparison.InvariantCultureIgnoreCase)
                &&
                p.Password == login.Password
            );

            if (user is null) return NotFound("User not found");

            var token = _autenticationService.GenerateJWTToken(user.Name);

            return Ok(token);
        }
    }
}