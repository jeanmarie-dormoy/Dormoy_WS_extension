using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTourism
{
    public class SystemEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SystemEnum) || t == typeof(SystemEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "places")
            {
                return SystemEnum.Places;
            }
            throw new Exception("Cannot unmarshal type SystemEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SystemEnum)untypedValue;
            if (value == SystemEnum.Places)
            {
                serializer.Serialize(writer, "places");
                return;
            }
            throw new Exception("Cannot marshal type SystemEnum");
        }

        public static readonly SystemEnumConverter Singleton = new SystemEnumConverter();
    }
}
