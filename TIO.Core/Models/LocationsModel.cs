using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web;
using System.Linq;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class LocationsModel : RenderModel
    {
        public List<LocationModel> Locations { get; private set; }
        public IEnumerable<TagModel> Tags { get; private set; }
        public LocationsModel(
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            bool isEnglish,
            int? tagId,
            string tag,
            IContentService contentService) : 
            base(content)
        {
            UmbracoHelper helper = new UmbracoHelper(UmbracoContext.Current, content);

            this.Tags = helper.TagQuery.GetAllTags(Constants.NodeAlias.TAG_GROUP);
             
            if (tagId.HasValue)
            {
                tag = this.Tags.First(x => x.Id == tagId.Value).Text;
            }
            else
            {
                tag = Constants.NodeAlias.DEFAULT_TAG;
            }
                

            this.Locations = new List<LocationModel>();
            
            var locations = helper.TagQuery.GetContentByTag(tag, Constants.NodeAlias.TAG_GROUP);

            this.Locations
                .AddRange(locations.Where(x => x.GetPropertyValue<bool>(Constants.Location.Properties.PUBLISHED))
                .Select(x => LocationFactory.Create(x, recommendationRepository, contentService, isEnglish, isDetails: false)));
        }
    }
}
