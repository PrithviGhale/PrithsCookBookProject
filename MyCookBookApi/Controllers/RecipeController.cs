using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;

[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRecipes()
    {
        return Ok(new List<Recipe>
        {
            new Recipe { Name = "Pasta", Ingredients = new List<string> { "Pasta", "Tomato Sauce" }, Steps = "Boil pasta." },
            new Recipe { Name = "Salad", Ingredients = new List<string> { "Lettuce", "Tomatoes" }, Steps = "Mix all ingredients." }
        });
    }
}
