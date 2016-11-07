using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public static class LocationFactory
    {
        public static LocationModel Create(
            IPublishedContent content, 
            IPublishedContent recommendationRepository,
            IContentService contentService,
            bool isEnglish,
            bool isDetails)
        {
            if (isEnglish)
                return new LocationUKModel(content, recommendationRepository, contentService, isDetails);

            return new LocationDKModel(content, recommendationRepository, contentService, isDetails);
        }
    }
}
