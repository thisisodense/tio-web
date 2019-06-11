using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using static TIO.Core.Models.Constants.Controllers.WriterArchive;

namespace TIO.Core.Models
{
    public class ArchiveModel : RenderModel
    {
        public string Headline { get; private set; }
        public string SubHeader { get; private set;  }
        public string Body { get; private set; }
        public int Id { get; private set; }
        public string Image { get; private set; }
        public DateTime PublishDate { private set; get; }
        public FILTER Filter { private set; get; }
        public ArchiveModel(
            IPublishedContent content, 
            int id, 
            string headline, 
            string subheader, 
            string body, 
            string image,
            DateTime publishedDate,
            FILTER filter) : base(content)
        {
            this.Id = id;
            this.Headline = headline;
            this.SubHeader = subheader;
            this.Body = body;
            this.Image = image;
            this.PublishDate = publishedDate;
            this.Filter = filter;
        }
    }
}
