using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class Lang
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iso6391")]
        public string Iso6391 { get; set; }

        [JsonProperty("iso6393")]
        public string Iso6393 { get; set; }

        [JsonProperty("defaulted")]
        public bool Defaulted { get; set; }
    }
}
