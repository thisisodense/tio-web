using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class WritersModel : RenderModel
    {
        public List<WriterModel> Writers { get; private set; }
        public string About { get; private set; }
        public WritersModel(
            IPublishedContent content, 
            bool isEnglish, 
            bool isDetails,
            IContentService contentService) : base(content)
        {
            this.Writers = new List<WriterModel>();
            IPublishedContent writerRepository = content.FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.WRITER_REPOSITORY);

            if (writerRepository == null)
                return;

            IEnumerable<IPublishedContent> writers = writerRepository
                .Children(x => x.IsVisible() && x.GetPropertyValue<bool>(Constants.Writer.Properties.SHOW));

            if (writers == null)
                return;

            this.About = writerRepository.GetPropertyValue<string>(Constants.Writer.Properties.ABOUT);

            IPublishedContent recommendationsRespository = content
                          .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            IPublishedContent articlesRespository = content
                          .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.ARTICLE_REPOSISTORY);

            IPublishedContent locationsRespository = content
                          .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.LOCATION_REPOSITORY);

            this.Writers.AddRange(writers.Select(x => WriterFactory.Create(x, recommendationsRespository, articlesRespository, locationsRespository, isEnglish, isDetails, contentService, Constants.Controllers.WriterArchive.FILTER.Recommendations, 0)));

        }
    }
}
