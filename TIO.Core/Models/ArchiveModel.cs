using System;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using static TIO.Core.Models.Constants.Controllers.WriterArchive;

namespace TIO.Core.Models
{
    public class ArchiveModel : RenderModel
    {
        public string Headline { get; private set; }
        public string SubHeader { get; private set; }
        public string Body { get; private set;  }
        public int Id { get; private set; }
        public string Image { get; private set; }
        public DateTime PublishDate { private set; get; }
        public FILTER Filter { private set; get; }
        public ArchiveModel(
            IPublishedContent content, 
            string headline, 
            string subheader, 
            string body, 
            string image,
            string crop,
            string publishedDate,
            FILTER filter) : base(content)
        {
            this.Id = content.Id;
            this.Headline = content.GetPropertyValue<string>(headline);
            this.SubHeader = content.GetPropertyValue<string>(subheader);
            this.Body = content.GetPropertyValue<string>(body);
            this.Image = content.GetCropUrl(image, crop);
            this.PublishDate = publishedDate == "" ? content.CreateDate :  content.GetPropertyValue<DateTime>(publishedDate);
            this.Filter = filter;
        }
    }
}
