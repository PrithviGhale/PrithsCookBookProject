namespace MyCookBookApi.Models
{
    public class RecipeSearchRequest
    {
        public string Keyword { get; set; }

        public List<string> Categories { get; set; } = new List<string>();
    }
}
