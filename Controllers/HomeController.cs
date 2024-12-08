using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context) 
            => Ok(context.Todos.ToList());

        [HttpGet("/{id}")]
        public IActionResult GetById([FromRoute]int id, [FromServices] AppDbContext context)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null)
                return NotFound();

            return Ok(todo);
        }

        [HttpPost("/")]
        public IActionResult Post([FromBody]TodoModel model, [FromServices] AppDbContext context)
        {
            context.Todos.Add(model);
            context.SaveChanges();

            return Created($"/{model.Id}", model);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] TodoModel model, [FromServices] AppDbContext context)
        {
            TodoModel? todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null)
                return NotFound();

            todo.Title = model.Title;
            todo.Done = model.Done;

            context.Todos.Update(todo);
            context.SaveChanges();

            return Ok(todo);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            TodoModel? todo = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null)
                return NotFound();

            context.Todos.Remove(todo);
            context.SaveChanges();

            return Ok(todo);
        }
    }
}
