using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplication3
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application.Add("ToplamZiyaretci", 0);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if ((int)Application["ToplamZiyaretci"] == 0)
            Application["ToplamZiyaretci"] = 1;
            else
         {
                int deger = (int)Application["ToplamZiyaretci"];
             deger += 1;
             Application["ToplamZiyaretci"] = deger;}
    
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(Server.MapPath("~/Hatalar.txt"), true); sw.WriteLine(DateTime.Now.ToString());
        }

        protected void Session_End(object sender, EventArgs e)
        {
            int deger = (int)Application["ToplamZiyaretci"];
            deger -= 1; Application["ToplamZiyaretci"] = deger;
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}