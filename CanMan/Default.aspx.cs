using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace CanMan
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["url"] != null)
            {
                ProcessUrl(Request.QueryString["url"]);
            }
        }

        private void ProcessUrl(string p)
        {
            var request = ProcessRequest(p);

            string result = ReadResponseContent(request);

            var regex = "<a href=\"/people/([^\"]*)\">";

            var match = Regex.Match(result, regex);

            if (match != null)
            {
                var username = match.Groups[1].Captures[0].Value;

                var urlPattern = "www.dailymile.com/routes/([0-9]*)-";

                var urlMatch = Regex.Match(p, urlPattern);

                var id = urlMatch.Groups[1].Captures[0].Value;

                Response.Redirect(String.Format("{0}/{1}", username, id));

            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("{0}", username.Text));

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
