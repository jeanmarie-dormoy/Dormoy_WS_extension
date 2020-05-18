using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class FeatureORS
    {
        [JsonProperty("bbox")]
        public double[] Bbox { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("properties")]
        public PropertiesORS Properties { get; set; }

        [JsonProperty("geometry")]
        public GeometryORS Geometry { get; set; }
    }
}
