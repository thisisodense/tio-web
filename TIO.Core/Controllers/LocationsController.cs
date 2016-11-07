using System.Web.Mvc;
using TIO.Mvc.Core;
using TIO.Core.Models;
using Umbraco.Core.Models;
using System.Linq;
using Umbraco.Web;

namespace TIO.Core.Controllers
{
    public class LocationsController : BaseController
    {
        public ActionResult Locations(int? tagId, string tag)
        {
            IPublishedContent recommendationRespository = Umbraco.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            LocationsModel model = new LocationsModel(
                                            CurrentPage,
                                            recommendationRespository, 
                                            Site.IsEnglish,
                                            tagId,
                                            tag,
                                            Services.ContentService);

            return CurrentTemplate(model);
        }
    }
}
