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

            this.Recommendations.AddRange(recommendations
                                    .Select(x => RecommendationFactory.Create(x, recommendationsRespository, contentService, isEnglish, isDetails)));


            this.Recommendations = this.Recommendations
                                            .OrderBy(x => x.StartDate)
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
