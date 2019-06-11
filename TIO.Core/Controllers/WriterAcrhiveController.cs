using System.Web.Mvc;
using TIO.Mvc.Core;
using TIO.Core.Models;
using Umbraco.Web.Models;
using Library.Tools;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;
using FILTER = TIO.Core.Models.Constants.Controllers.WriterArchive.FILTER;

namespace TIO.Core.Controllers
{
    public class WriterArchiveController : BaseController
    {
        public ActionResult WriterArchive(RenderModel model, int id, string name, FILTER filter = FILTER.Recommendations, int page = 1)
        {
            IPublishedContent recommendationsRespository = Umbraco.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            IPublishedContent articleRespository = Umbraco.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.ARTICLE_REPOSISTORY);

            IPublishedContent locationRespository = Umbraco.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.LOCATION_REPOSITORY);

            WriterModel writerModel = WriterFactory
                .Create(model.Content, recommendationsRespository, articleRespository, locationRespository, Site.IsEnglish, false, Services.ContentService, filter, page);

            string expectedName = writerModel.Name.ToSeoUrl();

            string actualName = (name ?? "").ToLower();

            // permanently redirect to the correct URL
            if (expectedName != actualName)
            {
                return RedirectToActionPermanent(Constants.Controllers.WriterArchive.NAME,
                                                 Constants.Controllers.WriterArchive.Actions.INDEX,
                                                 new
                                                 {
                                                     id = writerModel.Id,
                                                     name = expectedName,
                                                     filter
                                                 });
            }

            return CurrentTemplate(writerModel);
        }
    }
}
