using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyCookBookApi.Models
{
    [FirestoreData]
    public class Recipe
    {
        [FirestoreProperty]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string RecipeId { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;

        [FirestoreProperty]
        public string? TagLine { get; set; }

        [FirestoreProperty]
        public string? Summary { get; set; }

        [FirestoreProperty]
        public List<string> Ingredients { get; set; } = new();

        [FirestoreProperty]
        public List<string> Instructions { get; set; } = new();

        [FirestoreProperty(ConverterType = typeof(CategoryTypeConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CategoryType>? Categories { get; set; } = new();

        [FirestoreProperty(ConverterType = typeof(RecipeMediaConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RecipeMedia>? Media { get; set; } = new();
    }
}
