using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyCookBookApi.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; } // Auto-generated unique ID
        public string Name { get; set; } // Name of the recipe
        public string TagLine { get; set; } // Short tagline for the recipe
        public string Summary { get; set; } // Brief summary of the recipe
        public List<string> Ingredients { get; set; } = new List<string>(); // List of ingredients
        public List<string> Instructions { get; set; } = new List<string>(); // List of steps for preparation
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CategoryType>? Categories { get; set; } = new List<CategoryType>(); // Optional: Recipe categories
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RecipeMedia>? Media { get; set; } = new List<RecipeMedia>(); // Optional: Media URLs (images/videos)

        public Recipe() { }
    }
}