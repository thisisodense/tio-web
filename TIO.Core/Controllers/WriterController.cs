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
            IPublishedContent recommendationsRespository = Umbraco.TypedContentAtRoot().FirstOrDefault()
                         .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            WriterModel writerModel = WriterFactory.Create(model.Content, recommendationsRespository);

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
