using Xunit;
using MyCookBookApp.Models;
using System.Collections.Generic;

namespace MyCookBookApp.Tests
{
    public class RecipeModelTests
    {
        [Fact]
        public void RecipeModel_ShouldStoreDataCorrectly()
        {
            var recipe = new Recipe
            {
                Name = "Salad",
                Ingredients = new List<string> { "Lettuce", "Tomatoes", "Dressing" },
                Instructions = new List<string> { "Mix ingredients together." }
            };

            Assert.Equal("Salad", recipe.Name);
            Assert.Contains("Tomatoes", recipe.Ingredients);
            Assert.Contains("Mix ingredients together.", recipe.Instructions);
        }
    }
}
