using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web;

namespace TIO.Core.Models
{
    public class WritersModel : RenderModel
    {
        public List<WriterModel> Writers { get; private set; }
        public string About { get; private set; }
        public WritersModel(IPublishedContent content) : base(content)
        {
            this.Writers = new List<WriterModel>();
            IPublishedContent writerRepository = content.FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.WRITER_REPOSITORY);

            if (writerRepository == null)
                return;

            IEnumerable<IPublishedContent> writers = writerRepository.Children(x => x.IsVisible());

            if (writers == null)
                return;

            this.About = writerRepository.GetPropertyValue<string>(Constants.Writer.Properties.ABOUT);

            IPublishedContent recommendationsRespository = content
                          .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            this.Writers.AddRange(writers.Select(x => WriterFactory.Create(x, recommendationsRespository)));

        }
    }
}
