using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CanMan
{
    [JsonObject]
    public class Listing : IComparable
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }

        [JsonProperty("geoCode", NullValueHandling = NullValueHandling.Ignore)]
        public GeoCode GeoCode { get; set; }

        public int CompareTo(object obj)
        {
            Listing converted = obj as Listing;

            if (converted != null )
            {
                return converted.Id - this.Id;
            }
            else
            {
                return 1;
            }
        }
    }
}