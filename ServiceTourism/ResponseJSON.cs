using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTourism
{
    public partial class Results
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("position")]
        public double[] Position { get; set; }

        [JsonProperty("distance")]
        public long Distance { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("averageRating")]
        public long AverageRating { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("icon")]
        public Uri Icon { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

        [JsonProperty("having")]
        public object[] Having { get; set; }

        [JsonProperty("type")]
        public ItemType Type { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public Tag[] Tags { get; set; }

        [JsonProperty("openingHours", NullValueHandling = NullValueHandling.Ignore)]
        public OpeningHours OpeningHours { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("type")]
        public CategoryType Type { get; set; }

        [JsonProperty("system")]
        public SystemEnum System { get; set; }
    }

    public partial class OpeningHours
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("isOpen")]
        public bool IsOpen { get; set; }

        [JsonProperty("structured")]
        public Structured[] Structured { get; set; }
    }

    public partial class Structured
    {
        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("recurrence")]
        public string Recurrence { get; set; }
    }

    public partial class Tag
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("group")]
        public Group Group { get; set; }
    }

    public partial class Search
    {
        [JsonProperty("context")]
        public Context Context { get; set; }
    }

    public partial class Context
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("type")]
        public ItemType Type { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("position")]
        public double[] Position { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("bbox")]
        public double[] Bbox { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("postalCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PostalCode { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("stateCode")]
        public string StateCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
    }

    public enum SystemEnum { Places };

    public enum CategoryType { UrnNlpTypesCategory };

    public enum Group { Cuisine };

    public enum ItemType { UrnNlpTypesPlace };

    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, ServiceTourism.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, ServiceTourism.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                SystemEnumConverter.Singleton,
                CategoryTypeConverter.Singleton,
                GroupConverter.Singleton,
                ItemTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class SystemEnumConverter : JsonConverter
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

    internal class CategoryTypeConverter : JsonConverter
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

    internal class GroupConverter : JsonConverter
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

    internal class ItemTypeConverter : JsonConverter
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
    public partial class ResponseJSON
    {
        [JsonProperty("results")]
        public Results Results { get; set; }

        [JsonProperty("search")]
        public Search Search { get; set; }

        

       
    }
}
