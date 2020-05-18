using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class EngineORS
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("build_date")]
        public DateTimeOffset BuildDate { get; set; }

        [JsonProperty("graph_date")]
        public DateTimeOffset GraphDate { get; set; }
    }
}
