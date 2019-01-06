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
        public ActionResult Articles(int? tagId, string tag, int page = 1)
        {
            IPublishedContent articleRespository = Umbraco.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            ArticlesModel model = new ArticlesModel(
                                            CurrentPage,
                                            articleRespository,
                                            Site.IsEnglish,
                                            tagId,
                                            tag,
                                            page,
                                            Services.ContentService);

            return CurrentTemplate(model);
        }
    }
}
