using System;
using System.Collections.Generic;
using System.Text;

namespace GMapGeocoder.Containers
{
    /// <summary>
    /// Object to hold a US address in simpler format. Don't care about foreign addresses here.
    /// </summary>
    [Serializable]
    public class USAddress
    {
        string _id = "";
        AccuracyOptions _accuracy = AccuracyOptions.Unknown;

        string _fullText = "";
        string _street = "";
        string _city = "";
        string _county = "";
        string _stateCode = "";
        string _countryCode = "";
        string _zipCode = "";
        Point _coordinates = new Point();

        public USAddress() { }
       
        /// <summary>ID used by google to identify a placemark from a set of returned placemarks.</summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>Google's accuracy rating for the given address.</summary>
        public AccuracyOptions Accuracy
        {
            get { return _accuracy; }
            set { _accuracy = value; }
        }

        /// <summary>Full standardized address on a single line.</summary>
        public string FullText
        {
            get { return _fullText; }
            set { _fullText = value; }
        }

        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string County
        {
            get { return _county; }
            set { _county = value; }
        }

        public string StateCode
        {
            get { return _stateCode; }
            set { _stateCode = value; }
        }

        public string CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }

        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        public Point Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }
    }
}
