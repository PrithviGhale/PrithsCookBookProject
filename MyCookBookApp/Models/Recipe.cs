using System.Collections.Generic;

namespace MyCookBookApp.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; }
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string Summary { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
        public List<string> Categories { get; set; }

        public List<RecipeMedia> Media { get; set; }
    }

    public class RecipeMedia
    {
        public string Url { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
    }
}
