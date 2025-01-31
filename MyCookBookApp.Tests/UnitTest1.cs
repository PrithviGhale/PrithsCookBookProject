using Xunit;
using MyCookBookApp.Models;

public class RecipeTests
{
    [Fact]
    public void Recipe_ShouldStoreDataCorrectly()
    {
        var recipe = new Recipe
        {
            Name = "Test Recipe",
            Ingredients = new List<string> { "Ingredient1", "Ingredient2" },
            Steps = "Step1, Step2"
        };

        Assert.Equal("Test Recipe", recipe.Name);
        Assert.Contains("Ingredient1", recipe.Ingredients);
        Assert.Equal("Step1, Step2", recipe.Steps);
    }
}
