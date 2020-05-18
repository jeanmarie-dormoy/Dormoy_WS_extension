using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class ResponseORS
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public FeatureORS[] Features { get; set; }

        [JsonProperty("bbox")]
        public double[] Bbox { get; set; }

        [JsonProperty("metadata")]
        public MetadataORS Metadata { get; set; }
    }

}
