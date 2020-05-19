using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTourism
{
    public class CategoryTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CategoryType) || t == typeof(CategoryType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "urn:nlp-types:category")
            {
                return CategoryType.UrnNlpTypesCategory;
            }
            throw new Exception("Cannot unmarshal type CategoryType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CategoryType)untypedValue;
            if (value == CategoryType.UrnNlpTypesCategory)
            {
                serializer.Serialize(writer, "urn:nlp-types:category");
                return;
            }
            throw new Exception("Cannot marshal type CategoryType");
        }

        public static readonly CategoryTypeConverter Singleton = new CategoryTypeConverter();
    }
}
