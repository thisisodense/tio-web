using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class RecommendationUKModel : RecommendationModel
    {
        public override string HeadLineProperty { get { return Constants.Recommendation.Properties.HEADLINE_UK; } }
        public override string SubHeaderProperty { get { return Constants.Recommendation.Properties.SUB_HEADER_UK; } }
        public override string BodyProperty { get { return Constants.Recommendation.Properties.BODY_UK; } }

        public RecommendationUKModel(
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            IContentService contentService,
            bool isEnglish, 
            bool isDetails) : 
            base(content, recommendationRepository, contentService, isEnglish, isDetails) { }
    }
}
