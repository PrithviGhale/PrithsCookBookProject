using System.Collections.Generic;

namespace MyCookBookApp.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; } = string.Empty; // Unique ID for the recipe
        public string Name { get; set; } = string.Empty; // Name of the recipe
        public string TagLine { get; set; } = string.Empty; // Short tagline for the recipe
        public string Summary { get; set; } = string.Empty; // Brief summary of the recipe
        public List<string> Ingredients { get; set; } = new List<string>(); // List of ingredients
        public List<string> Instructions { get; set; } = new List<string>(); // List of steps for preparation
        public List<string> Categories { get; set; } = new List<string>(); // List of categories (e.g., Breakfast, Lunch)
        public List<string> Media { get; set; } = new List<string>(); // Optional: Media URLs (images/videos)
    }
}