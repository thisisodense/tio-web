using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public static class RecommendationFactory
    {
        public static RecommendationModel Create(
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            IContentService contentService,                     
            bool isEnglish,                                  
            bool isDetails)
        {
            if (isEnglish)
                return new RecommendationUKModel(content, recommendationRepository, contentService, isEnglish, isDetails);

            return new RecommendationDKModel(content, recommendationRepository, contentService, isEnglish, isDetails);
        }
    }
}
