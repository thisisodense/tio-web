using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace TIO.Mvc.Core
{
    public class BaseController : RenderMvcController
    {
        private SiteHelper _site;
        public SiteHelper Site { get { return _site ?? (_site = new SiteHelper()); } }
        public UmbracoHelper Helper { get { return new UmbracoHelper(UmbracoContext.Current); } }
    } 
}
