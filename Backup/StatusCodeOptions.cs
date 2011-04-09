using System;
using System.Collections.Generic;
using System.Text;

namespace GMapGeocoder
{
    /// <summary>
    /// A response code (similar to HTTP status codes) indicating whether the geocode request was successful or not. 
    /// </summary>
    public enum StatusCodeOptions
    {
        /// <summary>No errors occurred; the address was successfully parsed and its geocode has been returned.</summary>
        Success = 200,
        /// <summary>A directions request could not be successfully parsed.</summary>
        BadRequest = 400,
        /// <summary>A geocoding or directions request could not be successfully processed, yet the exact reason for the failure is not known.</summary>
        ServerError = 500,
        /// <summary>The HTTP q parameter was either missing or had no value. For geocoding requests, this means that an empty address was specified as input. For directions requests, this means that no query was specified in the input.</summary>
        MissingQueryOrAddress = 601,
        /// <summary>No corresponding geographic location could be found for the specified address. This may be due to the fact that the address is relatively new, or it may be incorrect.</summary>
        UnknownAddress = 602,
        /// <summary>The geocode for the given address or the route for the given directions query cannot be returned due to legal or contractual reasons.</summary>
        UnavailableAddress = 603,
        /// <summary>The GDirections object could not compute directions between the points mentioned in the query. This is usually because there is no route available between the two points, or because we do not have data for routing in that region.</summary>
        UnknownDirections = 604,
        /// <summary>The given key is either invalid or does not match the domain for which it was given.</summary>
        BadKey = 610,
        /// <summary>The given key has gone over the requests limit in the 24 hour period.</summary>
        TooManyQueries = 620
    }
}
