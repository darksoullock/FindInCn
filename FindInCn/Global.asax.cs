using FindInCn.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FindInCn
{
    public class MvcApplication : System.Web.HttpApplication
    {
        void Init()
        {
            var c = new CnContext();
            c.Shops.Add(
                new Shop()
                {
                    Name = "AliExpress",
                    SearchUrl = "http://aliexpress.com/wholesale?SearchText={0}",
                    MainPage = "http://ru.aliexpress.com/ru_home.htm"
                });

            c.Shops.Add(
                new Shop()
                {
                    Name = "GearBest",
                    SearchUrl = "http://www.gearbest.com/{0}-_gear/",
                    MainPage = "http://www.gearbest.com/"
                });
        }

        protected void Application_Start()
        {
            Init();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
