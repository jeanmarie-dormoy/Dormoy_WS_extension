using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class RootJSON
    {
        [JsonProperty("geocoding")]
        public Geocoding Geocoding { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public Feature[] Features { get; set; }

        [JsonProperty("bbox")]
        public double[] Bbox { get; set; }
    }
}
