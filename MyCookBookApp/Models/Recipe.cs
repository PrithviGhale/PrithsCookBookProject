using System.Collections.Generic;

namespace MyCookBookApp.Models
{
    public class Recipe
    {
        public string Name { get; set; } = string.Empty; 
        public List<string> Ingredients { get; set; } = new List<string>(); 
        public string Steps { get; set; } = string.Empty; 
    }
}
