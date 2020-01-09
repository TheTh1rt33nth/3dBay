using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace _3dBay2
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {


            
            routes.MapPageRoute(null, "list/{category}/{page}",
                                        "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "list/{page}", "~/Pages/Listing.aspx");
            routes.MapPageRoute("order", "orderlist/{page}", "~/Pages/OrderListing.aspx");
            routes.MapRoute(
                 name: "default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
            routes.MapRoute("control", "{controller}/{action}");
            routes.MapPageRoute(null, "orderlist", "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "list", "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "", "~/Pages/Listing.aspx");
            /*routes.MapRoute("default", "{controller}/{action}",
                 defaults: new { action = "Index", controller = "Home" });*/


        }
    }
}
