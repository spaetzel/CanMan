/*
 * Sharmil Y. Desai 
 */

using System;
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GMapGeocoder
{
    public class Util
    {
        /// <summary>
        /// Calls http://maps.google.com/maps/geo?output=xml&q=SomeAddress&key=SomeKey and returns US friendly objects
        /// </summary>
        /// <param name="address">Address you are trying to geocode.</param>
        /// <param name="key">Your google maps key.</param>
        /// <returns></returns>
        public static Containers.Results Geocode(string address, string key)
        {
            return GoogleObjectsToResults(DeserializeXml(GetXml(address, key)));
        }

        /// <summary>
        /// Calls http://maps.google.com/maps/geo?output=xml&q=SomeAddress&key=SomeKey and returns xml string
        /// </summary>
        /// <param name="address">Address you are trying to geocode.</param>
        /// <param name="key">Your google maps key.</param>
        /// <returns></returns>
        public static string GetXml(string address, string key)
        {            
            string url = string.Format("http://maps.google.com/maps/geo?output=xml&q={0}&key={1}", HttpUtility.UrlEncode(address), key);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }                  
        }

        /// <summary>
        /// Takes xml string from google maps http://maps.google.com/maps/geo?output=xml&q=SomeAddress&key=SomeKey call and returns corresponding objects.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Generated.kml DeserializeXml(string xml)
        {
            // HACK - don't want to figure out how to get this xml ns to work
            xml = xml.Replace("xmlns=\"urn:oasis:names:tc:ciq:xsdschema:xAL:2.0\"", "");
            
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Generated.kml));
                return (Generated.kml)serializer.Deserialize(reader);               
            }
        }

        /// <summary>
        /// Takes google objects and returns US friendly query results.
        /// </summary>
        /// <param name="kml">Deserialize xml objects from google.</param>
        /// <returns></returns>
        public static Containers.Results GoogleObjectsToResults(Generated.kml kml)
        {
            Containers.Results results = new GMapGeocoder.Containers.Results();
            results.StatusCode = (StatusCodeOptions)kml.Response.Status.code;
            results.Query = kml.Response.name;

            if (kml.Response.Placemark != null)
            {
                foreach (Generated.Placemark p in kml.Response.Placemark)
                {
                    results.Addresses.Add(PlacemarkToUSAddress(p));
                }
            }

            return results;
        }

        /// <summary>
        /// Fills in USAddress fields with google's placemark object.
        /// </summary>
        /// <param name="placemark">Generated placemark object from google maps xsd.</param>
        /// <returns></returns>
        public static Containers.USAddress PlacemarkToUSAddress(Generated.Placemark placemark)
        {
            Containers.USAddress address = new Containers.USAddress();

            address.ID = placemark.id;
            address.FullText = placemark.address;
            address.Coordinates = PlacemarkPointToPoint(placemark.Point);

            if (placemark.AddressDetails != null)
            {
                Generated.AddressDetails addressDetails = placemark.AddressDetails;
                address.Accuracy = (AccuracyOptions)addressDetails.Accuracy;

                if (addressDetails.Country != null)
                {
                    Generated.Country country = addressDetails.Country;
                    address.CountryCode = country.CountryNameCode;

                    if (country.AdministrativeArea != null)
                    {
                        Generated.AdministrativeArea adminArea = country.AdministrativeArea;
                        address.StateCode = adminArea.AdministrativeAreaName;

                        // If county was found then this will probably be null and locality will be on the sub admin area
                        if (adminArea.Locality != null)
                        {
                            Generated.Locality locality = adminArea.Locality;
                            address.City = locality.LocalityName;

                            if (locality.Thoroughfare != null)
                                address.Street = locality.Thoroughfare.ThoroughfareName;
                            if (locality.PostalCode != null)
                                address.ZipCode = locality.PostalCode.PostalCodeNumber;
                        }

                        if (adminArea.SubAdministrativeArea != null)
                        {
                            Generated.SubAdministrativeArea subAdminArea = adminArea.SubAdministrativeArea;
                            address.County = subAdminArea.SubAdministrativeAreaName;

                            // Think this should be city info
                            if (subAdminArea.Locality != null)
                            {
                                Generated.Locality locality = subAdminArea.Locality;
                                address.City = locality.LocalityName;

                                if (locality.Thoroughfare != null)
                                    address.Street = locality.Thoroughfare.ThoroughfareName;
                                if (locality.PostalCode != null)
                                    address.ZipCode = locality.PostalCode.PostalCodeNumber;
                            }
                        }
                    }
                }
            }

            return address;
        }

        /// <summary>
        /// Takes point from google containing coordinates in a string and returns point objects with lat/lng separated out.
        /// </summary>
        /// <param name="point">Google placemark point.</param>
        /// <returns>Point containing coordinates. Lat/Lng are set to min double value on parse errors.</returns>
        public static Containers.Point PlacemarkPointToPoint(Generated.Point point)
        {
            try
            {
                Containers.Point p = new GMapGeocoder.Containers.Point();
                p.Longitude = Convert.ToDouble(point.coordinates.Split(new char[] { ',' })[0]);
                p.Latitude = Convert.ToDouble(point.coordinates.Split(new char[] { ',' })[1]);
                

                // Don't return empty point if we fail here.
                try
                {
                    p.Unbounded = Convert.ToBoolean(Convert.ToInt32(point.coordinates.Split(new char[] { ',' })[2]));
                }
                catch { }

                return p;
            }
            catch
            {
                return new Containers.Point();
            }
        }
    }
}
