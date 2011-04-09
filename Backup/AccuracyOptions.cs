using System;
using System.Collections.Generic;
using System.Text;

namespace GMapGeocoder
{
    /// <summary>
    /// An attribute indicating how accurately we were able to geocode the given address.
    /// </summary>
    public enum AccuracyOptions
    {
        /// <summary>Unknown location.</summary>
        Unknown = 0,
        /// <summary>Country level accuracy.</summary>
        Country = 1,
        /// <summary>Region (state, province, prefecture, etc.) level accuracy.</summary>
        Region = 2,
        /// <summary>Sub-region (county, municipality, etc.) level accuracy.</summary>
        SubRegion = 3,
        /// <summary>Town (city, village) level accuracy.</summary>
        Town = 4,
        /// <summary>Post code (zip code) level accuracy.</summary>
        PostalCode = 5,
        /// <summary>Street level accuracy.</summary>
        Street = 6,
        /// <summary>Intersection level accuracy.</summary>
        Intersection = 7,
        /// <summary>Address level accuracy.</summary>
        Address = 8,
        /// <summary>Premise (building name, property name, shopping center, etc.) level accuracy.</summary>
        Premise = 9
    }
}
