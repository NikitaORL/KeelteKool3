using System;
using System.Data.Entity;   // ← ДОБАВЬ ЭТО
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using KeelteKool.Migrations; // ← ДОБАВЬ ЭТО
using KeelteKool.Models;     // ← ДОБАВЬ ЭТО

namespace KeelteKool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 🔥 АВТО-СОЗДАНИЕ И ОБНОВЛЕНИЕ БД
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>()
            );

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
