using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class GeometryORS
    {
        [JsonProperty("coordinates")]
        public double[][] Coordinates { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
