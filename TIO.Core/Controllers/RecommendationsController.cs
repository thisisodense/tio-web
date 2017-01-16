using System.Web.Mvc;
using TIO.Mvc.Core;
using TIO.Core.Models;

namespace ThisIsOdense.Website.Controllers
{
    public class RecommendationsController : BaseController
    {
        public ActionResult Recommendations()
        {
            var model = new RecommendationsModel(
                                CurrentPage, Site.IsEnglish, false, Services.ContentService);

            return CurrentTemplate(model);
        }

        public virtual ActionResult Tips()
        {
            return RedirectPermanent("http://m.me/ThisIsOdense");
        }
    }
}
