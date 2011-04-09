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
            if (Page.RouteData.Values["username"] != null)
            {
                string username = Page.RouteData.Values["username"].ToString();

                if (!string.IsNullOrEmpty(username))
                {
                    var repository = SharpSpeed.SharpSpeedRepository.Instance;

                    var user = repository.GetPerson(username);

                    var prefix = "http://sized.castroller.com/" + 48 + "/" + 48 + "/";
                    userImage.ImageUrl = user.PhotoUrl.Replace("http://", prefix);
                    userLink.NavigateUrl = user.Url;

                    
                    Page.Title = String.Format("{0}'s routes - CanMan", username);

                    heading.InnerText = String.Format("{0}'s dailymile routes", username);

                
                    var routes = repository.GetRoutes(username);

                    routesList.DataSource = from r in routes
                                            select new
                                            {
                                                Name = r.Name,
                                                Id = r.Id,
                                                Username = username
                                            };
                    routesList.DataBind();
                }
            }
        }
    }
}