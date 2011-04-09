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

                var routes = repository.GetRoutes(username);

                var route = routes.FirstOrDefault(r => r.Id == routeId);


                var coordinates = repository.GetRoute(routeId);

                var latLns = from c in coordinates
                             select String.Format("new google.maps.LatLng({0}, {1})", c[0], c[1]);

                routePointsLiteral.Text = String.Join(",", latLns.ToArray());


                YellowSharp.ApiKey = "crds3fc5v3hzaehdgn8gte3s";

                YellowSharp.UseSandbox = true;
                
                var categories = System.Configuration.ConfigurationManager.AppSettings["Categories"].Split(',');


                var address = Geocoder.GetAddress(coordinates.First()[0], coordinates.First()[1]);

                var result = YellowSharp.FindBusinesses(categories.First(), address);

                FindBusinessComplete(result);

            }

        }


        private void FindBusinessComplete(SearchResult results)
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
  infowindow{4}.open(map,marker{4});
}});", curListing.Name.Replace("'", "\\'"), curListing.Address.Street, curListing.GeoCode.Latitude, curListing.GeoCode.Longitude, curListing.Id);


            setPointsLiteral.Text = String.Join("\n", markers.ToArray());


        
        }
    }
}