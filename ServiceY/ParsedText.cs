using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceY
{
    public partial class ParsedText
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }
    }
}
