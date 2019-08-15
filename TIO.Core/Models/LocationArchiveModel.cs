using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace TIO.Core.Models
{
    public class LocationArchiveModel : RenderModel
    {
        public string LongDescription { get; private set; }
        public string Image { get; private set; }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int NumberOfRecommendations { get { return this.Total; } }
        public IEnumerable<RecommendationModel> Recommendations { get; private set; }
        public bool DisablePreviousButton { get; private set; }
        public int Total { get; private set; }
        public bool DisableNextButton { get; private set; }
        public int Page { get; private set; }
        private int pageSize = 5;

        public LocationArchiveModel(
            string longDescription,
            string image, 
            int id,
            string name,
            bool isEnglish,
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            IContentService contentService,
            int page
            ) : base(content)
        {
            this.LongDescription = longDescription;
            this.Image = image;
            this.Name = name;
            this.Id = id;
            this.Page = page;

            var recommendationContent = recommendationRepository
                .Children(x => x.IsVisible() && x.GetPropertyValue<int>(Constants.Recommendation.Properties.LOCATION) == this.Id);

            this.Total = recommendationContent.Count();

            this.DisablePreviousButton = page == 1 ? true : false;
            this.DisableNextButton = page * pageSize > this.Total ? true : false;

            this.Recommendations = recommendationContent
                .Select(x => RecommendationFactory.Create(x, recommendationRepository, contentService, isEnglish, false))
                .OrderByDescending(x => x.StartDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
