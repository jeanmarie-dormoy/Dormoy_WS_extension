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
    public enum SystemEnum { Places };
    public enum CategoryType { UrnNlpTypesCategory };
    public enum Group { Cuisine };
    public enum ItemType { UrnNlpTypesPlace };
    public partial class ResponseJSON
    {
        [JsonProperty("results")]
        public Results Results { get; set; }

        [JsonProperty("search")]
        public Search Search { get; set; }
    }
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
        public LocationJSON Location { get; set; }

        [JsonProperty("type")]
        public ItemType Type { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }
    }

    public partial class LocationJSON
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


    public partial class ResponseJSON
    {
        public static ResponseJSON FromJson(string json) => JsonConvert.DeserializeObject<ResponseJSON>(json, ServiceTourism.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ResponseJSON self) => JsonConvert.SerializeObject(self, ServiceTourism.Converter.Settings);
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
}
