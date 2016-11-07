using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Library.Mvc.ActionResult
{
    public class RssActionResult : System.Web.Mvc.ActionResult
    {
        public SyndicationFeed Feed { get; set; }
        public XDocument RssFeed { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";

           
            using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                if (Feed != null)
                {
                    Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(Feed);
                    rssFormatter.WriteTo(writer);
                }

                if (RssFeed != null)
                    RssFeed.WriteTo(writer);
            }
        }
    }
}
