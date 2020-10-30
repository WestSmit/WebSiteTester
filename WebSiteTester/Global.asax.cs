using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;

using WebSiteTester.BLL.Util;

using WebSiteTester.Util;

namespace WebSiteTester
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // DI
            NinjectModule services = new NinjectRegistrations();
            NinjectModule data = new NinjectDataRegistration("DefaultConnection");

            var kernel = new StandardKernel(services, data);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
