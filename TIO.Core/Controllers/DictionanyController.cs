using System.Globalization;
using Umbraco.Web;
using System.Linq;
using Umbraco.Web.WebApi;
using System.Threading;

namespace TIO.Core.Controllers
{
    public class DictionanyController : UmbracoApiController
    {
        public string GetTranslation(string label, string lang)
        {
            if (!CultureInfo.GetCultures(CultureTypes.AllCultures).Any(x => x.TwoLetterISOLanguageName == lang))
                return string.Empty;

            var cultureInfo = new CultureInfo(lang);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            UmbracoHelper helper = new UmbracoHelper(UmbracoContext.Current);

            return helper.GetDictionaryValue(label);
        }
    }
}

