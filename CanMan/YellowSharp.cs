using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace CanMan
{
    public static class YellowSharp
    {
        private static string _apiKey;

        public static string ApiKey
        {
            get { return YellowSharp._apiKey; }
            set { YellowSharp._apiKey = value; }
        }
        private static bool _useSandbox;

        public static bool UseSandbox
        {
            get { return YellowSharp._useSandbox; }
            set { YellowSharp._useSandbox = value; }
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


        public static SearchResult FindBusinesses(string what, string where)
        {
            try
            {

                string url = String.Format("http://api.sandbox.yellowapi.com/FindBusiness/?what={0}&where={1}&fmt=JSON&pgLen=40&apikey={2}&UID=127.0.0.1", what, where, ApiKey);

                using (var resp = ProcessRequest(url))
                {
                    var respContent = ReadResponseContent(resp);
                    var notes = JsonConvert.DeserializeObject<SearchResult>(respContent);
                    return notes;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}