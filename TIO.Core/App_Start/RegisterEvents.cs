using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using Umbraco.Core;

namespace TIO.Core
{
    /// <summary>
    /// https://our.umbraco.org/Documentation/Reference/Events/application-startup
    /// </summary>
    public class RegisterEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UmbracoApplicationBase.ApplicationInit += UmbracoApplicationBase_ApplicationInit;
        }

        /// <summary>
        /// Bind to the events of the HttpApplication
        /// </summary>
        void UmbracoApplicationBase_ApplicationInit(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            app.PreRequestHandlerExecute += Application_PreRequestHandlerExecute;
        }

        private void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            string lang = new HttpContextWrapper(HttpContext.Current).Request.RequestContext.RouteData.Values["lang"] as string;
            if (string.IsNullOrEmpty(lang))
                return;

            if (!CultureInfo.GetCultures(CultureTypes.AllCultures).Any(x => x.TwoLetterISOLanguageName == lang))
                return;

            var cultureInfo = new CultureInfo(lang);

            cultureInfo.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

    }
}
