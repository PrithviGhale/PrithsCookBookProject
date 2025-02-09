using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MyCookBookApi.Models;

namespace MyCookBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly List<Recipe> _recipes = new List<Recipe>
        {
            new Recipe
            {
                Name = "Pasta",
                Ingredients = new List<string> { "Tomato Sauce", "Pasta", "Cheese" },
                Steps = "Boil pasta and mix with sauce."
            },
            new Recipe
            {
                Name = "Salad",
                Ingredients = new List<string> { "Lettuce", "Tomatoes", "Olive Oil" },
                Steps = "Chop ingredients and mix."
            }
        };

        [HttpGet]
        public IActionResult GetRecipes()
        {
            return Ok(_recipes);
        }

        [HttpGet("search")]
        public IActionResult SearchRecipes([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(_recipes); // Return all recipes if no query is provided
            }

            var matchingRecipes = _recipes
                .Where(r => r.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            r.Ingredients.Any(i => i.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                            r.Steps.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(matchingRecipes);
        }
    }
}