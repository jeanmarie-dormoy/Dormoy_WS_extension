using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class MetadataORS
    {
        [JsonProperty("attribution")]
        public string Attribution { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("query")]
        public QueryORS Query { get; set; }

        [JsonProperty("engine")]
        public EngineORS Engine { get; set; }
    }
}
