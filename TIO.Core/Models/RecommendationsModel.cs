using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;
using Library.Tools;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class RecommendationsModel : RenderModel
    {
        public List<RecommendationModel> Recommendations { get; private set; }
        public int ThisWeeksEditorId { get; private set; }
        public string ThisWeeksEditorName { get; private set; }
        public string ThisWeeksEdtiorPhoto { get; private set; }
        public string WeekDescription { get; private set; }
        public string WeekDescriptionEN { get; private set; }
        public string MainWriterName
        {
            get
            {
                return Recommendations
                            .GroupBy(x => x.WriterName)
                            .OrderByDescending(x => x.Count())
                            .FirstOrDefault()
                            .FirstOrDefault()
                            .WriterName;
            }
        }

        public int MainWriterId
        {
            get
            {
                return Recommendations
                            .GroupBy(x => x.WriterName)
                            .OrderByDescending(x => x.Count())
                            .FirstOrDefault()
                            .FirstOrDefault()
                            .WriterId;
            }
        }

        public bool HaveGuestWriter { get { return Recommendations.Any(x => x.IsGuestWriter); } }

        public RecommendationModel GuestRecommendation
        {
            get
            {
                return Recommendations.FirstOrDefault(x => x.IsGuestWriter);
            }
        }

        public RecommendationsModel(
            IPublishedContent content, 
            bool isEnglish, 
            bool isDetails,
            IContentService contentService) : base (content)
        {
            this.Recommendations = new List<RecommendationModel>();

            var recommendationsRespository = content
                                    .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY);

            if (recommendationsRespository == null)
                return;

            var recommendations = recommendationsRespository
                                        .Children(x => x.IsVisible() &&
                                                  
                                                   DateTime.UtcNow.Date >= x.GetPropertyValue<DateTime>(Constants.Recommendation.Properties.START_DATE).GetFirstDayOfWeek(CultureInfo.CurrentCulture) &&
                                                   DateTime.UtcNow.Date <= x.GetPropertyValue<DateTime>(Constants.Recommendation.Properties.START_DATE).GetLastDayOfWeek(CultureInfo.CurrentCulture));

            if (recommendations == null)
                return;

            UmbracoHelper helper = new UmbracoHelper(UmbracoContext.Current, content);

            IPublishedContent writer = helper.TypedContent(recommendationsRespository.GetPropertyValue<int>(Constants.Recommendations.Properties.EDITOR_OF_WEEK));

            if (writer != null)
            {
                this.ThisWeeksEditorName = writer.GetPropertyValue<string>(Constants.Writer.Properties.NAME);
                this.ThisWeeksEditorId = writer.Id;
                this.ThisWeeksEdtiorPhoto = writer.GetCropUrl(Constants.Writer.Properties.IMAGE, Constants.Crop.MINITURE_CROP);
            }

            this.WeekDescription = recommendationsRespository.GetPropertyValue<string>(Constants.Recommendations.Properties.WEEK_DESCRIPTION);
            this.WeekDescriptionEN = recommendationsRespository.GetPropertyValue<string>(Constants.Recommendations.Properties.WEEK_DESCRIPTION_EN);

            this.Recommendations.AddRange(recommendations
                                    .Select(x => RecommendationFactory.Create(x, recommendationsRespository, contentService, isEnglish, isDetails)));


            this.Recommendations = this.Recommendations
                                            .OrderByDescending(x => x.IsGuestWriter)
                                            .ThenBy(x => x.StartDate)
                                            .Take(6)
                                            .ToList();

            foreach(var recommendation in this.Recommendations)
            {
                if (this.Recommendations.IndexOf(recommendation) == 1)
                    recommendation.IsFirstRecommendation = true;

                recommendation.MainWriter = this.MainWriterName;
                recommendation.MainWriterId = this.MainWriterId;
                recommendation.HaveGuestWriter = this.HaveGuestWriter;
            }
           
        }
    }
}
