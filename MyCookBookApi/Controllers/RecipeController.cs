using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
using System.Collections.Generic;

namespace MyCookBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // GET: api/recipe
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetAllRecipes()
        {
            var recipes = _recipeService.GetAllRecipes();
            return Ok(recipes);
        }

        // GET: api/recipe/{id}
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipeById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Recipe ID is required.");

            var recipe = _recipeService.GetRecipeById(id);
            if (recipe == null)
                return NotFound($"Recipe with ID '{id}' not found.");

            return Ok(recipe);
        }

        // POST: api/recipe/search
        [HttpPost("search")]
        public ActionResult<IEnumerable<Recipe>> SearchRecipes([FromBody] RecipeSearchRequest searchRequest)
        {
            if (searchRequest == null || string.IsNullOrWhiteSpace(searchRequest.Keyword))
                return BadRequest("Keyword is required.");

            searchRequest.Categories ??= new List<string>();

            var results = _recipeService.SearchRecipes(searchRequest);
            return Ok(results);
        }

        // POST: api/recipe
        [HttpPost]
        public ActionResult<Recipe> CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null || string.IsNullOrWhiteSpace(recipe.Name))
                return BadRequest("Recipe data is invalid.");

            recipe.RecipeId = Guid.NewGuid().ToString();
            _recipeService.AddRecipe(recipe);

            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.RecipeId }, recipe);
        }

        // PUT: api/recipe/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRecipe(string id, [FromBody] Recipe recipe)
        {
            if (string.IsNullOrWhiteSpace(id) || recipe == null || string.IsNullOrWhiteSpace(recipe.Name))
                return BadRequest("Invalid input.");

            var updated = _recipeService.UpdateRecipe(id, recipe);
            if (!updated)
                return NotFound($"Recipe with ID '{id}' not found.");

            return NoContent();
        }

        // DELETE: api/recipe/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Recipe ID is required.");

            var deleted = _recipeService.DeleteRecipe(id);
            if (!deleted)
                return NotFound($"Recipe with ID '{id}' not found.");

            return NoContent();
        }
    }
}
