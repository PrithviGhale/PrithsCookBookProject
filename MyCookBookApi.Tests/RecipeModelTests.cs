﻿using Xunit;
using MyCookBookApi.Models;
using System.Collections.Generic;

namespace MyCookBookApi.Tests
{
    public class RecipeModelTests
    {
        [Fact]
        public void RecipeModel_ShouldStoreDataCorrectly()
        {
            var recipe = new Recipe
            {
                Name = "Pasta",
                Ingredients = new List<string> { "Pasta", "Tomato Sauce" },
                Steps = "Boil pasta."
            };

            Assert.Equal("Pasta", recipe.Name);
            Assert.Contains("Tomato Sauce", recipe.Ingredients);
            Assert.Equal("Boil pasta.", recipe.Steps);
        }
    }
}
