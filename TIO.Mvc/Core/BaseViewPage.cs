using System;
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

        public string BannerId { get { return "bannerCrop" + new Random().Next(1, 7).ToString(); } }



        public override void Execute()
        {
           throw new NotImplementedException();
        }
    }
}