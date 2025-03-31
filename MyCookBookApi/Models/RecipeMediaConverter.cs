using Google.Cloud.Firestore;
using System.Collections.Generic;
using System.Linq;

namespace MyCookBookApi.Models
{
    public class RecipeMediaConverter : IFirestoreConverter<List<RecipeMedia>>
    {
        public object ToFirestore(List<RecipeMedia> mediaList)
        {
            return mediaList.Select(media => new Dictionary<string, object>
            {
                { "Url", media.Url ?? string.Empty },
                { "Type", media.Type ?? string.Empty },
                { "Order", media.Order }
            }).ToList();
        }

        public List<RecipeMedia> FromFirestore(object value)
        {
            if (value is List<object> list)
            {
                return list.OfType<Dictionary<string, object>>()
                    .Select(dict => new RecipeMedia
                    {
                        Url = dict.ContainsKey("Url") ? dict["Url"].ToString() : string.Empty,
                        Type = dict.ContainsKey("Type") ? dict["Type"].ToString() : string.Empty,
                        Order = dict.ContainsKey("Order") ? Convert.ToInt32(dict["Order"]) : 0
                    }).ToList();
            }
            return new List<RecipeMedia>();
        }
    }
}