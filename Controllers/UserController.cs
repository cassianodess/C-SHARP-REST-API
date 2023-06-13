using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Repositories;

namespace Todo.Controllers {

    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase {


        [Authorize]
        [HttpGet]
        public IActionResult ListAll([FromServices] IUserRepository repository) {
            return Ok(repository.ListAll());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult FindById([FromServices] IUserRepository repository, string id) {
            return Ok(repository.FindById(new Guid(id)));
        }

        [HttpPost]
        public IActionResult Create([FromServices] IUserRepository repository, [FromBody] UserModel user) {

            if (!ModelState.IsValid) {
                return BadRequest();
            }
            repository.Create(user);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update([FromServices] IUserRepository repository, string id, [FromBody] UserModel user) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            repository.Update(new Guid(id), user);
            return Ok();

        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromServices] IUserRepository repository, string id) {
            repository.Delete(new Guid(id));
            return Ok();
        }

    }
}