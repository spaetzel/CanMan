using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CanMan
{
    public partial class Routes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Page.RouteData.Values["username"].ToString();

            var repository = SharpSpeed.SharpSpeedRepository.Instance;

            var routes = repository.GetRoutes(username);

            routesList.DataSource = routes;
            routesList.DataBind();
        }
    }
}