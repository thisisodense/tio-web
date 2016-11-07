using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Library.Tools;
using System.Diagnostics;

namespace Library.Mvc.Extensions
{
    public static class UrlHelperExtensions
    {
        public static MvcHtmlString FullUrlForRecommendation(this UrlHelper urlHelper, string baseUrl, int? id, string headline, string language = "da")
        {
            return MvcHtmlString.Create(string.Format("http://www.{0}/{1}/{2}/{3}", baseUrl, language, id, headline.ToSeoUrl()));
        }
    }
}
