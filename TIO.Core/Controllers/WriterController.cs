using System.Web.Mvc;
using TIO.Mvc.Core;
using TIO.Core.Models;
using Umbraco.Web.Models;
using Library.Tools;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;

namespace TIO.Core.Controllers
{
    public class WriterController : BaseController
    {
        public ActionResult Writer(RenderModel model, int id, string name)
        {

            IPublishedContent recommendationsRespository = Helper.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            IPublishedContent articleRespository = Helper.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.ARTICLE_REPOSISTORY);

            IPublishedContent locationRespository = Helper.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.LOCATION_REPOSITORY);

            WriterModel writerModel = WriterFactory.Create(model.Content, recommendationsRespository, articleRespository, locationRespository, Site.IsEnglish, false, Services.ContentService, Constants.Controllers.WriterArchive.FILTER.Recommendations, 0);

            string expectedName = writerModel.Name.ToSeoUrl();

            string actualName = (name ?? "").ToLower();

            // permanently redirect to the correct URL
            if (expectedName != actualName)
            {
                return RedirectToActionPermanent(Constants.Controllers.Writer.NAME,
                                                 Constants.Controllers.Writer.Actions.WRITER,
                                                 new
                                                 {
                                                     id = writerModel.Id,
                                                     name = expectedName
                                                 });
            }

            return CurrentTemplate(writerModel);
        }
    }
}
