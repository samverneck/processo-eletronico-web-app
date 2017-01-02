using StackExchange.Profiling;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApp
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

        protected void Application_BeginRequest()
        {
            if (!Request.Url.Host.Contains("processoeletronico.es.gov.br"))
            //if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        //protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        //{
        //    HttpApplication app = sender as HttpApplication;
        //    string acceptEncoding = app.Request.Headers["Accept-Encoding"];
        //    Stream prevUncompressedStream = app.Response.Filter;

        //    if (app.Context.CurrentHandler == null || app.Context.CurrentHandler is MiniProfilerHandler)
        //        return;
        //}
    }
}
