using System;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web;
using System.Linq;

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
        public int NumberOfRecommendation { get; private set; }
        public string Link { get; private set; }
        public string WriterSince { get; set; }
        public WriterModel(
            IPublishedContent content,
            IPublishedContent recommendationsRespository) : base(content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            if(recommendationsRespository != null)
            {
                var recommendations = recommendationsRespository.Children(x => x.IsVisible() && x.GetPropertyValue<int>(Constants.Recommendation.Properties.WRITER) == content.Id);
                this.NumberOfRecommendation = recommendations.Count();
            }

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
