using System.Collections.Generic;

namespace MyCookBookApi.Models
{
    public class RecipeCreateRequest
    {
        public string Name { get; set; }
        public string TagLine { get; set; }
        public string Summary { get; set; }
        public List<string> Ingredients { get; set; } = new();
        public List<string> Instructions { get; set; } = new();
        public List<CategoryType>? Categories { get; set; } = new();
        public List<RecipeMedia>? Media { get; set; } = new();
    }
}
