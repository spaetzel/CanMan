using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CanMan
{
    [JsonObject]
    public class Address
    {
        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("prov", NullValueHandling = NullValueHandling.Ignore)]
        public string Prov{get; set;}

        [JsonProperty("pcode", NullValueHandling = NullValueHandling.Ignore)]
        public string Pcode { get; set; }

    }
}