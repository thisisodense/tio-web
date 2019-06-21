using System.Web.Mvc;
using TIO.Mvc.Core;
using TIO.Core.Models;
using Umbraco.Web.Models;
using Library.Tools;
using Umbraco.Core.Models;
using System.Linq;
using Umbraco.Web;

namespace TIO.Core.Controllers
{
    public class LocationController : BaseController
    {

        public ActionResult Location(
            RenderModel model,
            int id,
            string name)
        {
            IPublishedContent recommendationsRespository = Helper.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            LocationModel locationModel =  LocationFactory.Create(model.Content, recommendationsRespository, Services.ContentService, Site.IsEnglish, isDetails:true);

            string expectedName = locationModel.Title.ToSeoUrl();

            string actualName = (name ?? "").ToLower();

            // permanently redirect to the correct URL
            if (expectedName != actualName)
            {
                return RedirectToActionPermanent(Constants.Controllers.Location.NAME,
                                                 Constants.Controllers.Location.Actions.LOCATION,
                                                 new
                                                 {
                                                     id = locationModel.Id,
                                                     name = expectedName
                                                 });
            }

            return CurrentTemplate(locationModel);
        }
    }
}
