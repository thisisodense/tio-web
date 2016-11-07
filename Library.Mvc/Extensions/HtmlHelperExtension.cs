using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Library.Mvc.Extensions
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString IsActive(this HtmlHelper htmlHelper, params string[] urls)
        {
            if (urls.Contains(HttpContext.Current.Request.Url.PathAndQuery))
                return MvcHtmlString.Create("active");

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString Hide(this HtmlHelper htmlHelper, params string[] urls)
        {
            if (urls.Contains(HttpContext.Current.Request.Url.PathAndQuery))
                return MvcHtmlString.Create("hide");

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString IsOpen(this HtmlHelper htmlHelper, params string[] urls)
        {
            if (urls.Contains(HttpContext.Current.Request.Url.PathAndQuery))
                return MvcHtmlString.Create("open");

            return MvcHtmlString.Empty;
        }

        public static T GetRouteDataValue<T>(this HtmlHelper htmlHelper, string key)
        {
            object value = new HttpContextWrapper(HttpContext.Current).Request.RequestContext.RouteData.Values[key];
            
            if (value == null)
                return default(T);

            return (T)value;
        }

        public static string FirstCharToUpper(this HtmlHelper htmlHelper, string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }
    }
}
