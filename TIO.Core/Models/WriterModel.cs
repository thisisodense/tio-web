using System;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web;
using System.Linq;
using System.Collections.Generic;
using Umbraco.Core.Services;
using FILTER = TIO.Core.Models.Constants.Controllers.WriterArchive.FILTER;

namespace TIO.Core.Models
{
    public class WriterModel : RenderModel
    {
        public int Id { get; private set; }
        public string Background { get; private set; }
        public string Interests { get; private set; }
        public string Title { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Image { get; private set; }
        public string DetailImage { get; private set; }
        public string WriterVideo { get; private set; }
        public int NumberOfRecommendation { get { return this.Recommendations.Count; } }
        public int NumberOfArticles { get { return this.Articles.Count; } }
        public int NumberOfLocations { get { return this.Locations.Count; } }
        public string Link { get; private set; }
        public string WriterSince { get; set; }
        public List<IPublishedContent> Recommendations { get; private set; }
        public List<IPublishedContent> Articles { get; private set; }
        public List<IPublishedContent> Locations { get; private set; }
        public List<ArchiveModel> Archive { get; private set; }
        public FILTER Filter { get; private set; }
        public int Page { get; private set; }
        public int Total { get; private set; }
        public bool DisablePreviousButton { get; private set; }
        public bool DisableNextButton { get; private set; }
        private int pageSize = 5;
        public WriterModel(
            IPublishedContent content,
            IPublishedContent recommendationsRespository,
            IPublishedContent articleRespository,
            IPublishedContent locationRepository,
            bool isEnglish,
            bool isDetails,
            IContentService contentService,
            FILTER filter,
            int page) : base(content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            this.Filter = filter;
            this.Page = page;

            this.Recommendations = new List<IPublishedContent>();
            this.Locations = new List<IPublishedContent>();
            this.Articles = new List<IPublishedContent>();

            if (recommendationsRespository != null)
            {
                this.Recommendations.AddRange(recommendationsRespository.Children(x => x.IsVisible() && x.GetPropertyValue<int>(Constants.Recommendation.Properties.WRITER) == content.Id));
            }

            if(articleRespository != null)
            {
                this.Articles
                       .AddRange(articleRespository.Children(x => x.IsVisible() && x.GetPropertyValue<int>(Constants.Article.Properties.WRITER_LONGREAD) == content.Id));
            }

            if (locationRepository != null)
            {
                this.Locations
                        .AddRange(locationRepository.Children(x => x.IsVisible() && x.GetPropertyValue<int>(Constants.Location.Properties.WRITER) == content.Id));

            }

            switch (this.Filter)
            {
                case FILTER.Recommendations:
                    this.Archive = this.Recommendations
                        .Select(x => new ArchiveModel(x, 
                            isEnglish ? Constants.Recommendation.Properties.HEADLINE_UK : Constants.Recommendation.Properties.HEADLINE,
                            isEnglish ? Constants.Recommendation.Properties.SUB_HEADER_UK : Constants.Recommendation.Properties.SUB_HEADER,
                            isEnglish ? Constants.Recommendation.Properties.BODY_UK : Constants.Recommendation.Properties.BODY,
                            Constants.Recommendation.Properties.IMAGE_URL,
                            Constants.Crop.RECOMMENDATION_IMAGE,
                            Constants.Recommendation.Properties.START_DATE,
                            this.Filter
                            )).ToList();
                    break;
                case FILTER.Articles:
                    this.Archive = this
                        .Articles.Select(x => new ArchiveModel(x, 
                            isEnglish ? Constants.Article.Properties.headlineEnglish : Constants.Article.Properties.HEADLINE,
                            "",
                            isEnglish ? Constants.Article.Properties.summaryEnglish : Constants.Article.Properties.SUMMARY,
                            Constants.Article.Properties.Image,
                            Constants.Crop.RECOMMENDATION_IMAGE,
                            Constants.Article.Properties.PUBLISH_DATE,
                            this.Filter)).ToList();
                    break;
                case FILTER.Locations:
                    this.Archive = this.Locations
                        .Select(x => new ArchiveModel(x, 
                            Constants.Location.Properties.TITLE, 
                            isEnglish ? Constants.Location.Properties.SHORT_DESCRIPTION_EN : Constants.Location.Properties.SHORT_DESCRIPTION,
                            isEnglish ? Constants.Location.Properties.LONG_DESCRIPTION_EN : Constants.Location.Properties.LONG_DESCRIPTION,
                            Constants.Location.Properties.IMAGE,
                            Constants.Crop.RECOMMENDATION_IMAGE,
                            "",
                            this.Filter
                            )).ToList();
                    break;
            }

            this.Total = this.Archive.Count();

            this.DisablePreviousButton = page == 1 ? true : false;
            this.DisableNextButton = page * pageSize > this.Total ? true : false;

            this.Archive = this.Archive
                    .OrderByDescending(x => x.PublishDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();

            this.Id = content.Id;
            this.Background = content.GetPropertyValue<string>(Constants.Writer.Properties.BACKGROUND);
            this.Email = content.GetPropertyValue<string>(Constants.Writer.Properties.EMAIL);
            this.Name = content.GetPropertyValue<string>(Constants.Writer.Properties.NAME);
            this.Image = content.GetCropUrl(Constants.Writer.Properties.IMAGE, Constants.Crop.WRITER_IMAGE);
            this.DetailImage = content.GetCropUrl(Constants.Writer.Properties.IMAGE, Constants.Crop.LOCATION_IMAGE);
            this.Title = content.GetPropertyValue<string>(Constants.Writer.Properties.TITLE);
            this.Interests = content.GetPropertyValue<string>(Constants.Writer.Properties.INTERESTS);
            this.WriterVideo = content.GetPropertyValue<string>(Constants.Writer.Properties.WRITER_VIDEO);
            this.Link = content.GetPropertyValue<string>(Constants.Writer.Properties.LINK);
            this.WriterSince = content.GetPropertyValue<string>(Constants.Writer.Properties.WRITER_SINCE);
        }
    }
}
