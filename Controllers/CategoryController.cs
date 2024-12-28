using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")] // localhost:port/v1/categories
        public async Task<IActionResult> GetAsync([FromServices] AppDataContext context)
        {
            try
            {
                List<Category> categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>>(error:"Não foi possível recuperar os dados requeridos."));
            }

        }

        [HttpGet("v1/categories/{id:int}")] // localhost:port/v1/categories/{id}
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id,
                                                      [FromServices] AppDataContext context)
        {
            try
            {
                var category = await context.Categories
                                       .FirstOrDefaultAsync(x => x.ID == id);

                if (category is null)
                    return NotFound(new ResultViewModel<Category>(error:"Conteúdo não encotrado."));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>(error:"Falha interna no servidor."));
            }
        }

        [HttpPost("v1/categories")] // localhost:port/v1/categories
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel viewModel,
                                                   [FromServices] AppDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));

            try
            {
                Category category = new()
                {
                    ID = 0,
                    Posts = [],
                    Name = viewModel.Name,
                    Slug = viewModel.Slug.ToLower(),
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.ID}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>($"05xe9 - Não foi possível incluir a categoria.\n{ex.Message}"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe10 - Falha interna do servidor."));
            }
        }

        [HttpPut("v1/categories/put/{id:int}")] // localhost:port/v1/categories
        public async Task<IActionResult> PutAsync([FromRoute] int id,
                                                  [FromBody] EditorCategoryViewModel viewModel,
                                                  [FromServices] AppDataContext context)
        {
            try
            {
                Category? category = await context.Categories
                                                  .FirstOrDefaultAsync(x => x.ID == id);

                if (category is null)
                    return NotFound(new ResultViewModel<Category>(error: "Conteúdo não encotrado."));

                category.Name = viewModel.Name;
                category.Slug = viewModel.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>($"05xe11 - Não foi possível alterar a categoria.\n{ex.Message}"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe12 - Falha interna do servidor."));
            }
        }

        [HttpPut("v1/categories/{id:int}")] // localhost:port/v1/categories
        public async Task<IActionResult> DeleteAsync([FromRoute] int id,
                                                     [FromServices] AppDataContext context)
        {
            try
            {
                var category = await context.Categories
                                       .FirstOrDefaultAsync(x => x.ID == id);

                if (category is null)
                    return NotFound(new ResultViewModel<Category>(error: "Conteúdo não encotrado."));

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>($"05xe13 - Não foi possível excluir a categoria.\n{ex.Message}"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe14 - Falha interna do servidor."));
            }
        }
    }
}
