using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;


namespace ServiceY
{
    public partial class Query
    {
        [JsonProperty("parsed_text")]
        public ParsedText ParsedText { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("lang")]
        public Lang Lang { get; set; }

        [JsonProperty("querySize")]
        public long QuerySize { get; set; }
    }
}
