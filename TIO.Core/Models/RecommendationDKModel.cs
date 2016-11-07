using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class RecommendationDKModel : RecommendationModel
    {
        public override string HeadLineProperty { get { return Constants.Recommendation.Properties.HEADLINE; } }
        public override string SubHeaderProperty { get { return Constants.Recommendation.Properties.SUB_HEADER; } }
        public override string BodyProperty { get { return Constants.Recommendation.Properties.BODY; } }

        public RecommendationDKModel(
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            IContentService contentService,
            bool isEnglish, 
            bool isDetails) :
            base(content, recommendationRepository, contentService, isEnglish, isDetails) { }
    }
}
