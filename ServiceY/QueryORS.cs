using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class QueryORS
    {
        [JsonProperty("coordinates")]
        public double[][] Coordinates { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("maneuvers")]
        public bool Maneuvers { get; set; }
    }
}
