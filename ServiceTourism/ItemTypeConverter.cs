using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTourism
{
    public class ItemTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ItemType) || t == typeof(ItemType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "urn:nlp-types:place")
            {
                return ItemType.UrnNlpTypesPlace;
            }
            throw new Exception("Cannot unmarshal type ItemType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ItemType)untypedValue;
            if (value == ItemType.UrnNlpTypesPlace)
            {
                serializer.Serialize(writer, "urn:nlp-types:place");
                return;
            }
            throw new Exception("Cannot marshal type ItemType");
        }

        public static readonly ItemTypeConverter Singleton = new ItemTypeConverter();
    }
}
