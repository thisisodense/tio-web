using System.Linq;
using System.Web.Mvc;
using TIO.Core.Models;
using TIO.Mvc.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace TIO.Core.Controllers
{
    public class ArticlesController : BaseController
    {
        public ActionResult Articles(int? tagId, string tag)
        {
            IPublishedContent articleRespository = Umbraco.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            ArticlesModel model = new ArticlesModel(
                                            CurrentPage,
                                            articleRespository,
                                            Site.IsEnglish,
                                            tagId,
                                            tag,
                                            Services.ContentService);

            return CurrentTemplate(model);
        }
    }
}
