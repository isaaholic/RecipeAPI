using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeAPI.Data;
using RecipeAPI.Models;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecipesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        [HttpGet("category/{category}")]
        public ActionResult<IEnumerable<Recipe>> GetRecipesByCategory(string category)
        {
            return _context.Recipes.Where(r => r.Category.ToLower() == category.ToLower()).ToList();
        }

        [HttpGet("cuisine/{cuisine}")]
        public ActionResult<IEnumerable<Recipe>> GetRecipesByCuisine(string cuisine)
        {
            return _context.Recipes.Where(r => r.Cuisine.ToLower() == cuisine.ToLower()).ToList();
        }

        [HttpGet("ingredient")]
        public ActionResult<IEnumerable<Recipe>> GetRecipesByIngredient(string ingredient)
        {
            return _context.Recipes.Where(r => r.Ingredients.ToLower().Contains(ingredient.ToLower())).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private async Task<ActionResult<Recipe>> GetRecipeById(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(Guid id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/recipes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
