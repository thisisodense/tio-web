using System.Web.Mvc;
using TIO.Mvc.Core;
using TIO.Core.Models;
using Library.Tools;
using Umbraco.Web.Models;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;

namespace TIO.Core.Controllers
{
    public class RecommendationController : BaseController
    {
        public ActionResult Recommendation(
            RenderModel model, 
            int id, 
            string name)
        {
            IPublishedContent recommendationRespository = Helper.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            RecommendationModel recommendationModel = RecommendationFactory.Create(model.Content, recommendationRespository, Services.ContentService, Site.IsEnglish, isDetails: true);

            // make sure the productName for the route matches the encoded product name
            string expectedName = recommendationModel.HeadLine.ToSeoUrl();

            string actualName = (name ?? "").ToLower();

            // permanently redirect to the correct URL
            if (expectedName != actualName)
            {
                return RedirectToActionPermanent(Constants.Controllers.Recommendation.NAME,
                                                 Constants.Controllers.Recommendation.Actions.RECOMMENDATION, 
                                                 new
                                                 {
                                                     id = recommendationModel.Id,
                                                     name = expectedName
                                                 });
            }

            return CurrentTemplate(recommendationModel);
           
        }
    }
}
