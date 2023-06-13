using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Repositories;

namespace Todo.Controllers {

    [Authorize]
    [ApiController]
    [Route("/api/todos")]
    public class TodoController : ControllerBase {

        [HttpGet()]
        public IActionResult ListAll([FromServices] ITodoRepository repository) {
            return Ok(repository.ListAll());
        }

        [HttpGet("{id}")]
        public IActionResult FindById([FromServices] IUserRepository repository, string id) {
            return Ok(repository.FindById(new Guid(id)));
        }


        [HttpPost]
        public IActionResult Create([FromBody] TodoModel todo, [FromServices] ITodoRepository repository) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
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