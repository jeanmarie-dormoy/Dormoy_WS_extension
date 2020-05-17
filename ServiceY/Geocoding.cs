using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class Geocoding
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("attribution")]
        public Uri Attribution { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("engine")]
        public Engine Engine { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
