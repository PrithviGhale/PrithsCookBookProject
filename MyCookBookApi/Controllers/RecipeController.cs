using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyCookBookApi.Models;

namespace MyCookBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRecipes()
        {
            var recipes = new List<Recipe>
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

            return Ok(recipes);
        }
    }
}
