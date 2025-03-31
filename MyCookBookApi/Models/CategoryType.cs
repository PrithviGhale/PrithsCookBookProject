using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CategoryType
{
    Breakfast,
    Lunch,
    Dinner,
    Dessert,
    Snack,
    Vegan,
    Vegetarian
}
