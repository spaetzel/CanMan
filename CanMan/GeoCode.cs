
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CanMan
{
    [JsonObject]
    public class GeoCode
    {
        [JsonProperty("latitude", NullValueHandling = NullValueHandling.Ignore)]
        public double Latitude{get; set;}

        [JsonProperty("longitude", NullValueHandling = NullValueHandling.Ignore)]
        public double Longitude { get; set; }
    }
}