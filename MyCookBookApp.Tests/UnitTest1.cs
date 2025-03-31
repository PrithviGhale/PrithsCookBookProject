using Xunit;
using MyCookBookApp.Models;
using System.Collections.Generic;

public class RecipeTests
{
    [Fact]
    public void Recipe_ShouldStoreDataCorrectly()
    {
        var recipe = new Recipe
        {
            Name = "Test Recipe",
            Ingredients = new List<string> { "Ingredient1", "Ingredient2" },
            Instructions = new List<string> { "Step1", "Step2" }
        };

        Assert.Equal("Test Recipe", recipe.Name);
        Assert.Contains("Ingredient1", recipe.Ingredients);
        Assert.Contains("Step1", recipe.Instructions);
        Assert.Contains("Step2", recipe.Instructions);
    }
}
