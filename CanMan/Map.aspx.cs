using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using System.Device;

namespace CanMan
{
    public partial class Map : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int routeId;

            if (Int32.TryParse(Page.RouteData.Values["routeId"].ToString(), out routeId))
            {

                string username = Page.RouteData.Values["username"].ToString();

                var repository = SharpSpeed.SharpSpeedRepository.Instance;

                var user = repository.GetPerson(username);

                var prefix = "http://sized.castroller.com/" + 48 + "/" + 48 + "/";
                userImage.ImageUrl = user.PhotoUrl.Replace("http://", prefix);
                userLink.NavigateUrl = user.Url;

                var routes = repository.GetRoutes(username);


                var route = routes.FirstOrDefault(r => r.Id == routeId);

                Page.Title = String.Format("{0} - CanMan", route.Name);

                heading.InnerText = String.Format("{0}'s {1} route", username, route.Name );


                Master.Menu.Items.Add(new MenuItem()
                {
                    Text = String.Format("All of {0}'s routes", username),
                    NavigateUrl = String.Format("/{0}", username)
                });
            

                var coordinates = repository.GetRoute(routeId);

                var latLns = from c in coordinates
                             select String.Format("new google.maps.LatLng({0}, {1})", c[0], c[1]);

                routePointsLiteral.Text = String.Join(",", latLns.ToArray());


                YellowSharp.ApiKey = "crds3fc5v3hzaehdgn8gte3s";

                YellowSharp.UseSandbox = true;
                
                var categories = System.Configuration.ConfigurationManager.AppSettings["Categories"].Split(',');

                var midIndex = (int)Math.Round(coordinates.Count() / 2.0);

                var midPoint = coordinates.ElementAt( midIndex );

                midPointLiteral.Text = latLns.ElementAt(midIndex);

                var address = Geocoder.GetAddress(midPoint[0], midPoint[1]);

                List<Listing> allResults = new List<Listing>();

                foreach (var curCategory in categories)
                {
                    allResults.AddRange(YellowSharp.FindBusinesses(curCategory, address));
                }

                var result = from l in allResults.Distinct<Listing>()
                             where l.Address != null && l.GeoCode != null
                             select l;

                FindBusinessComplete(result);

            }

        }


        private void FindBusinessComplete(IEnumerable<Listing> results)
        {

            var markers = from curListing in results
                          select String.Format(@"
                                var myLatlng{4} = new google.maps.LatLng({2},{3});

var contentString{4} = '<div id=\'content\'>'+
    '<div id=\'siteNotice\'>'+
    '</div>'+
    '<h1 id=\'firstHeading\' class=\'firstHeading\'>{0}</h1>'+
    '<div id=\'bodyContent\'>'+
    '<p>{1}</p>'+
    '</div>'+
    '</div>';

var infowindow{4} = new google.maps.InfoWindow({{
    content: contentString{4}
}});

var marker{4} = new google.maps.Marker({{
    position: myLatlng{4},
    map: map,
    title:'{0}'
}});

google.maps.event.addListener(marker{4}, 'click', function() {{
if( lastWindow != null )
{{
    lastWindow.close();
}}
  infowindow{4}.open(map,marker{4});
lastWindow = infowindow{4};
}});", CleanString(curListing.Name), CleanString( curListing.Address.Street), curListing.GeoCode.Latitude, curListing.GeoCode.Longitude, curListing.Id);


            setPointsLiteral.Text = String.Join("\n", markers.ToArray());


        
        }

        private string CleanString(string input)
        {
            return input.Replace("'", "\\'");
        }
    }
}