using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTourism
{
    public class GroupConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Group) || t == typeof(Group?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "cuisine")
            {
                return Group.Cuisine;
            }
            throw new Exception("Cannot unmarshal type Group");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Group)untypedValue;
            if (value == Group.Cuisine)
            {
                serializer.Serialize(writer, "cuisine");
                return;
            }
            throw new Exception("Cannot marshal type Group");
        }

        public static readonly GroupConverter Singleton = new GroupConverter();
    }
}
