using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCookBookApi.Models
{
    public class CategoryTypeConverter : IFirestoreConverter<List<CategoryType>>
    {
        public object ToFirestore(List<CategoryType> categories)
        {
            return categories?.Select(c => c.ToString()).ToList() ?? new List<string>();
        }

        public List<CategoryType> FromFirestore(object value)
        {
            if (value is List<object> list)
            {
                return list.Select(item =>
                    Enum.TryParse(item.ToString(), out CategoryType category)
                        ? category
                        : CategoryType.Dinner).ToList();
            }

            return new List<CategoryType>(); 
        }
    }
}
