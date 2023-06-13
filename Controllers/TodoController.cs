using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Repositories;
namespace Todo.Controllers {

    [Authorize]
    [ApiController]
    [Route("/api/todos")]
    public class TodoController : ControllerBase {

        [HttpGet]
        public IActionResult ListAll([FromServices] ITodoRepository repository) {
            return Ok(repository.ListAll(new Guid(User.Identity.Name)));
        }

        [HttpGet("{id}")]
        public IActionResult FindById([FromServices] IUserRepository repository, string id) {
            return Ok(repository.FindById(new Guid(id)));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody] TodoModel todo, [FromServices] ITodoRepository repository) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            todo.UserId = new Guid(User.Identity.Name);
            repository.Create(todo);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromServices] ITodoRepository repository, string id, [FromBody] TodoModel todo) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            repository.Update(new Guid(id), todo);
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromServices] ITodoRepository repository, string id) {
            repository.DeleteById(new Guid(id));
            return Ok();
        }



    }
}