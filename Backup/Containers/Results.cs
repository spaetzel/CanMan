using System;
using System.Collections.Generic;
using System.Text;

namespace GMapGeocoder.Containers
{
    /// <summary>
    /// Container to hold geocode results in easier to use format.
    /// </summary>
    [Serializable]
    public class Results
    {
        StatusCodeOptions _statusCode = StatusCodeOptions.MissingQueryOrAddress;
        string _query = "";
        List<Containers.USAddress> _addresses = new List<USAddress>();

        public Results() { }

        /// <summary>A response code (similar to HTTP status codes) indicating whether the geocode request was successful or not. </summary>
        public StatusCodeOptions StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }

        /// <summary>Geocode query that was sent to google.</summary>
        public string Query
        {
            get { return _query; }
            set { _query = value; }
        }

        /// <summary>List of addresses returned by geocode query.</summary>
        public List<Containers.USAddress> Addresses
        {
            get { return _addresses; }
            set { _addresses = value; }
        }
    }
}
