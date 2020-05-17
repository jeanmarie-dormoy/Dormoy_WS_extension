using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class Properties
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("gid")]
        public string Gid { get; set; }

        [JsonProperty("layer")]
        public string Layer { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("source_id")]
        public string SourceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("match_type")]
        public string MatchType { get; set; }

        [JsonProperty("accuracy")]
        public string Accuracy { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_gid")]
        public string CountryGid { get; set; }

        [JsonProperty("country_a")]
        public string CountryA { get; set; }

        [JsonProperty("macroregion", NullValueHandling = NullValueHandling.Ignore)]
        public string Macroregion { get; set; }

        [JsonProperty("macroregion_gid", NullValueHandling = NullValueHandling.Ignore)]
        public string MacroregionGid { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("region_gid")]
        public string RegionGid { get; set; }

        [JsonProperty("region_a")]
        public string RegionA { get; set; }

        [JsonProperty("macrocounty", NullValueHandling = NullValueHandling.Ignore)]
        public string Macrocounty { get; set; }

        [JsonProperty("macrocounty_gid", NullValueHandling = NullValueHandling.Ignore)]
        public string MacrocountyGid { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("county_gid")]
        public string CountyGid { get; set; }

        [JsonProperty("localadmin", NullValueHandling = NullValueHandling.Ignore)]
        public string Localadmin { get; set; }

        [JsonProperty("localadmin_gid", NullValueHandling = NullValueHandling.Ignore)]
        public string LocaladminGid { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("locality_gid")]
        public string LocalityGid { get; set; }

        [JsonProperty("continent")]
        public string Continent { get; set; }

        [JsonProperty("continent_gid")]
        public string ContinentGid { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
