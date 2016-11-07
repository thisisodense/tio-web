using System;
using Umbraco.Web.Mvc;

namespace TIO.Mvc.Core
{
    public class ThisIsOdenseTemplatePage : UmbracoTemplatePage
    {
        private SiteHelper _site;

        public SiteHelper Site { get { return _site ?? (_site = new SiteHelper()); } }
        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
