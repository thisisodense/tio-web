using System.Web.Mvc;
using TIO.Mvc.Core;
using TIO.Core.Models;

namespace TIO.Core.Controllers
{
    public class AboutController : BaseController
    {
        public ActionResult About()
        {
            return CurrentTemplate(new WritersModel(CurrentPage));
        }
    }
}
