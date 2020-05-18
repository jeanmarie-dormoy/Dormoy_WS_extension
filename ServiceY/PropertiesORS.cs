using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class PropertiesORS
    {
        [JsonProperty("summary")]
        public SummaryORS Summary { get; set; }

        [JsonProperty("way_points")]
        public long[] WayPoints { get; set; }
    }
}
