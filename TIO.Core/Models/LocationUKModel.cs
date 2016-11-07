using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class LocationUKModel : LocationModel
    {
        public override string LongDescriptionProperty { get { return Constants.Location.Properties.LONG_DESCRIPTION_EN; } }

        public override string ShortDescriptionProperty { get { return Constants.Location.Properties.SHORT_DESCRIPTION_EN; } }

        public LocationUKModel(
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            IContentService contentService,
            bool isDetails) : 
            base(content, recommendationRepository, contentService, isDetails)
        {

        }
    }
}
