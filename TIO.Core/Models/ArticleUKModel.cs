using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class ArticleUKModel : ArticleModel
    {
        public override string HeadLineProperty { get { return Constants.Article.Properties.headlineEnglish; } }
        public override string SummaryProperty { get { return Constants.Article.Properties.summaryEnglish; } }
        public ArticleUKModel(
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            IContentService contentService,
            bool isDetails) : 
            base(content, recommendationRepository, contentService, isDetails)
        {

        }
    }
}
