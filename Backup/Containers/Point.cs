using System;
using System.Collections.Generic;
using System.Text;

namespace GMapGeocoder.Containers
{
    /// <summary>Point in geographical coordinates longitude and latitude.</summary>
    [Serializable]
    public class Point
    {
        double _latitude = Double.MinValue;
        double _longitude = Double.MinValue;
        bool _unbounded = false;

        public Point() { }

        /// <param name="latitude">Latitude coordinate in degrees, as a number between -90 and +90. If the unbounded flag was set, this coordinate can be outside this interval.</param>
        /// <param name="longitude">Longitude coordinate in degrees, as a number between -180 and +180. If the unbounded flag was set, this coordinate can be outside this interval.</param>
        public Point(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        /// <param name="latitude">Latitude coordinate in degrees, as a number between -90 and +90. If the unbounded flag was set, this coordinate can be outside this interval.</param>
        /// <param name="longitude">Longitude coordinate in degrees, as a number between -180 and +180. If the unbounded flag was set, this coordinate can be outside this interval.</param>
        /// <param name="unbounded">If the unbounded flag is true, then the numbers are not constrained, otherwise latitude was clamped to lie between -90 degrees and +90 degrees, and longitude is wrapped to lie between -180 degrees and +180 degrees.</param>
        public Point(double latitude, double longitude, bool unbounded)
        {
            _latitude = latitude;
            _longitude = longitude;
            _unbounded = unbounded;
        }

        /// <summary>Latitude coordinate in degrees, as a number between -90 and +90. If the unbounded flag was set, this coordinate can be outside this interval.</summary>
        public double Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        /// <summary>Longitude coordinate in degrees, as a number between -180 and +180. If the unbounded flag was set, this coordinate can be outside this interval.</summary>
        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        /// <summary>If the unbounded flag is true, then the numbers are not constrained, otherwise latitude was clamped to lie between -90 degrees and +90 degrees, and longitude is wrapped to lie between -180 degrees and +180 degrees.</summary>
        public bool Unbounded
        {
            get { return _unbounded; }
            set { _unbounded = value; }
        }
    }
}
