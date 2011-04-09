using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace CanMan
{
    public static class Geocoder
    {
        // http://maps.googleapis.com/maps/api/geocode/json?latlng=40.714224,-73.961452&sensor=false

        public static string GetAddress(double latitude, double longitude)
        {
            var url = String.Format("http://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&sensor=false", latitude, longitude);

            var response = ProcessRequest(url);

            var json = ReadResponseContent(response);

            JObject o = JObject.Parse(json);

            var all = from result in o["results"].Children()
                      select result["formatted_address"].ToString();

            return all.FirstOrDefault();
        }


        /// <summary>
        /// Generic method to process a request to dailymile.
        /// All publicly expose methods which interact with the store are processed though this.
        /// </summary>
        /// <param name="requestPath">The path to the request to be processed</param>
        /// <param name="method">The HTTP method for the request</param>
        /// <param name="content">The content to send in the request</param>
        /// <param name="queryParams">Queryparameters for the request</param>
        /// <returns>An HttpWebResponse continaing details returned from dailymile</returns>
        private static HttpWebResponse ProcessRequest(string url)
        {
            try
            {
               

                var req = WebRequest.Create(url) as HttpWebRequest;
                req.CookieContainer = new CookieContainer();
                req.Method = "get";

                req.ContentLength = 0;
                

                return (HttpWebResponse)req.GetResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads the content from the response object
        /// </summary>
        /// <param name="resp">The response to be processed</param>
        /// <returns>A string of the response content</returns>
        private static string ReadResponseContent(HttpWebResponse resp)
        {
            if (resp == null) throw new ArgumentNullException("resp");
            using (var sr = new StreamReader(resp.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
}