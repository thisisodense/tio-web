using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class ArticleDKModel : ArticleModel
    {
        public override string HeadLineProperty { get { return Constants.Article.Properties.HEADLINE; } }
        public override string SummaryProperty { get { return Constants.Article.Properties.SUMMARY; } }
        public ArticleDKModel(
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            IContentService contentService,
            bool isDetails) : 
            base(content, recommendationRepository, contentService, isDetails)
        {

        }
    }
}
