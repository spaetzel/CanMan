using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;

namespace CanMan
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup

            RegisterRoutes(RouteTable.Routes);

        }


        private void RegisterRoutes(RouteCollection routes)
        {


            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("User routes", "{username}", "~/Routes.aspx", true, null,
                new RouteValueDictionary { { "username", "[a-zA-Z].*" } });

            routes.MapPageRoute("Route map", "{username}/{routeId}", "~/Map.aspx");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
