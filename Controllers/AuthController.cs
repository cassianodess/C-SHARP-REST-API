using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Repositories;

namespace Todo.Controllers {

    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase {


        [HttpPost]
        public IActionResult Login([FromServices] IAuthRepository repository, [FromBody] AuthModel auth) {

            if (!ModelState.IsValid) {
                return BadRequest();
            }

            UserModel user = repository.Auth(auth);

            return Ok(new {
                data = user.ToDTO(),
                token = repository.GenerateToken(user)
            });
        }


    }
}