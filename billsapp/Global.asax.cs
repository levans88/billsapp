using billsapp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace billsapp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Custom view engine for rendering partials from folders specified in CustomViewEngine.cs
            ViewEngines.Engines.Add(new CustomViewEngine());

            // Added to resolved this error: "The model backing the 'ApplicationDbContext' context has changed 
            // since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<ApplicationDbContext>(null);
        }
    }
}
