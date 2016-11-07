using System.Web;
using Library.Tools;

namespace TIO.Mvc.Core
{
    public class SiteHelper
    {
        public bool IsEnglish { get; set; }
        public string BaseUrl { get { return HttpContext.Current.Request.Url.TopLevelDomainname(); } }

        public SiteHelper()
        {
            string lang = new HttpContextWrapper(HttpContext.Current).Request.RequestContext.RouteData.Values["lang"] as string;

            if (!string.IsNullOrEmpty(lang) && lang == "en")
                IsEnglish = true;
        }
    }
}
