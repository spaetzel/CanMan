using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                
                string gpxString =  repository.GetRouteGpx(routeId);

                gpx.Value = gpxString;


            }
        }
    }
}