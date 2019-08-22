using System.Web.Mvc;
using TIO.Mvc.Core;
using TIO.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Core.Models;
using System.Linq;
using Library.Tools;
using Umbraco.Web;

namespace ThisIsOdense.Website.Controllers
{
    public class LocationArchiveController : BaseController
    {
        public ActionResult LocationArchive(
            RenderModel model,
            int id, 
            string name,
            int page = 1)
        {
            IPublishedContent recommendationsRespository = Helper.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            LocationArchiveModel locationArchiveModel = new LocationArchiveModel(
                 Site.IsEnglish ? model.Content.GetPropertyValue<string>(Constants.Location.Properties.LONG_DESCRIPTION_EN) : model.Content.GetPropertyValue<string>(Constants.Location.Properties.LONG_DESCRIPTION),
                 model.Content.GetCropUrl(Constants.Location.Properties.IMAGE, Constants.Crop.LOCATION_IMAGE),
                 model.Content.Id,
                 model.Content.GetPropertyValue<string>(Constants.Location.Properties.TITLE),
                 Site.IsEnglish,
                 model.Content,
                 recommendationsRespository,
                 Services.ContentService,
                 page,
                 model.Content.GetPropertyValue<bool>(Constants.Location.Properties.PUBLISHED));

            string expectedName = locationArchiveModel.Name.ToSeoUrl();

            string actualName = (name ?? "").ToLower();

            // permanently redirect to the correct URL
            if (expectedName != actualName)
            {
                return RedirectToActionPermanent(Constants.Controllers.LocationArchive.NAME,
                                                 Constants.Controllers.LocationArchive.Actions.LOCATION_ARCHIVE,
                                                 new
                                                 {
                                                     id = locationArchiveModel.Id,
                                                     name = expectedName
                                                 });
            }

            return CurrentTemplate(locationArchiveModel);
        }
    }
}
