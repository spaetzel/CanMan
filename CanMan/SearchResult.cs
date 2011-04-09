using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CanMan
{
    [JsonObject]
    public class SearchResult: IEnumerable<Listing>
    {
        /// <summary>
        /// The private collection of items
        /// </summary>
        [JsonProperty("listings", NullValueHandling = NullValueHandling.Ignore)]
        private Listing[] Items { get; set; }


        #region IEnumerable<T> methods
        public IEnumerator<Listing> GetEnumerator()
        {
            return Items.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion      
    }
}