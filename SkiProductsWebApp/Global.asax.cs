using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Optimization;
using System.Web.Routing;

namespace SkiProductsWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        // EventArgs e
        public void Session_Start(string sender1, string sender2)
        {
            HttpContext.Current.Session.Add("role", sender1);
            HttpContext.Current.Session.Add("userid", sender2);
        }

        public void Session_End()
        {
            HttpContext.Current.Session.Remove("role");
            HttpContext.Current.Session.Remove("userid");
        }

    }

    //public class CustomFilter : ActionFilterAttribute, IAuthorizationFilter
    //{
    //    public void OnAuthorization(AuthorizationContext filterContext)
    //    {
    //        if (Convert.ToString(filterContext.HttpContext.Session["role"]) == "2" || string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["role"])))
    //        {
    //            filterContext.Result = new RedirectToRouteResult(
    //           new RouteValueDictionary
    //           {
    //                { "controller", "Home" },
    //                { "action", "Index" }
    //           });
    //        }
            
    //    }

      

    //}
}
