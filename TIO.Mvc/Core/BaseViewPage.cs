using System;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace TIO.Mvc.Core
{
    public class BaseViewPage<T> : UmbracoViewPage<T>
    {
        private SiteHelper _site;

        public SiteHelper Site { get { return _site ?? (_site = new SiteHelper()); } }

        protected override void InitializePage()
        {
            base.InitializePage();
        }

        public UmbracoHelper Helper { get { return new UmbracoHelper(UmbracoContext.Current); } }

        public string BannerId { get { return "bannerCrop" + new Random().Next(1, 7).ToString(); } }



        public override void Execute()
        {
           throw new NotImplementedException();
        }
    }
}