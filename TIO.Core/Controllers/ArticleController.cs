using Library.Tools;
using System.Linq;
using System.Web.Mvc;
using TIO.Core.Models;
using TIO.Mvc.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace TIO.Core.Controllers
{
    public class ArticleController : BaseController
    {
        public ActionResult Article(
            RenderModel model,
            int id,
            string name)
        {
            IPublishedContent recommendationsRespository = Umbraco.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            ArticleModel articleModel = ActicleFactory.Create(model.Content, recommendationsRespository, Services.ContentService, Site.IsEnglish, isDetails: true);

            string expectedName = articleModel.Headline.ToSeoUrl();

            string actualName = (name ?? "").ToLower();

            // permanently redirect to the correct URL
            if (expectedName != actualName)
            {
                return RedirectToActionPermanent(Constants.Controllers.Article.NAME,
                                                 Constants.Controllers.Article.Actions.ARTICLE,
                                                 new
                                                 {
                                                     id = articleModel.Id,
                                                     name = expectedName
                                                 });
            }

            return CurrentTemplate(articleModel);
        }
    }
}
