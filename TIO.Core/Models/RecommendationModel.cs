using System;
using Umbraco.Core.Models;
using Umbraco.Web;
using Library.Tools;
using System.Globalization;
using Umbraco.Web.Models;
using System.Threading;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public abstract class RecommendationModel : RenderModel
    {
        public abstract string HeadLineProperty { get; }
        public abstract string SubHeaderProperty { get; }
        public abstract string BodyProperty { get; }
        public bool IsGuestWriter { get; private set; }
        public int WriterId { get; private set; }
        public string WriterName { get; private set; }
        public string WriterUrl { get; private set; }
        public string WriterImage { get; private set; }
        public string WriterBackground { get; private set; }
        public string HeadLine { get; private set; }
        public string SubHeader { get; private set; }
        public string Body { get; private set; }
        public LocationModel Location { get; private set; }
        public string Organizer { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int Week { get { return StartDate.GetIso8601WeekOfYear(); } }
        public decimal Price { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public DateTime? DoorOpens { get; private set; }
        public string EventType { get; private set; }
        public string LinkToEvent { get; private set; }
        public string TicketUrl { get; private set; }
        public string ImageUrl { get; private set; }
        public string FacebookImageUrl { get; private set; }
        public int Id { get; private set; }
        public bool IsEnglish { get; private set; }
        public bool IsDetails { get; private set; }
        public string MainWriter { get; set; }
        public int MainWriterId { get; set; }
        public bool HaveGuestWriter { get; set; }
        public bool IsFirstRecommendation { get; set; }
        public int OldId { get; private set; }
        public int TranslatorId { get; private set; }
        public string TranslatorName { get; private set; }

        public int GetId()
        {
            return this.OldId == 0 ? this.Id : this.OldId;
        }

        public DateTime GetDate()
        {
            return EndDate != DateTime.MinValue && StartDate <= DateTimeOffset.UtcNow.Date && EndDate >= DateTimeOffset.UtcNow.Date ? DateTimeOffset.UtcNow.Date : StartDate.Date;
        }
        public bool IsCurrent
        {
            get
            {
                bool isCurrent = false;
                if (DateTimeOffset.UtcNow.Date >= StartDate.GetFirstDayOfWeek(CultureInfo.CurrentCulture)
                    && DateTimeOffset.UtcNow.Date <= StartDate.GetLastDayOfWeek(CultureInfo.CurrentCulture))
                {
                    isCurrent = true;
                }

                return isCurrent;
            }
        }

        public UmbracoHelper Helper { get { return new UmbracoHelper(UmbracoContext.Current); }}

        public RecommendationModel(
            IPublishedContent content,
            IPublishedContent recommendationRepository, 
            IContentService contentService,
            bool isEnglish, 
            bool isDetails) : base(
                content, 
                Thread.CurrentThread.CurrentUICulture)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            this.IsDetails = isDetails;
            this.IsEnglish = isEnglish;
            this.HeadLine = content.GetPropertyValue<string>(HeadLineProperty);
            this.SubHeader = content.GetPropertyValue<string>(SubHeaderProperty);
            this.Body = content.GetPropertyValue<string>(BodyProperty);
            this.Id = content.Id;
            this.IsGuestWriter = content.GetPropertyValue<bool>(Constants.Recommendation.Properties.GUEST_WRITER);
            this.WriterName = content.GetPropertyValue<string>(Constants.Recommendation.Properties.GUEST_WRITER_NAME);
            this.WriterUrl = content.GetPropertyValue<string>(Constants.Recommendation.Properties.GUEST_WRITER_LINK);
            this.Organizer = content.GetPropertyValue<string>(Constants.Recommendation.Properties.ORGANIZER);
            this.StartDate = content.GetPropertyValue<DateTime>(Constants.Recommendation.Properties.START_DATE);
            this.EndDate = content.GetPropertyValue<DateTime>(Constants.Recommendation.Properties.END_DATE);
            this.Price = content.GetPropertyValue<decimal>(Constants.Recommendation.Properties.PRICE);
            this.StartTime = content.GetPropertyValue<DateTime>(Constants.Recommendation.Properties.START_TIME);
            this.EndTime = content.GetPropertyValue<DateTime?>(Constants.Recommendation.Properties.END_TIME);
            this.DoorOpens = content.GetPropertyValue<DateTime?>(Constants.Recommendation.Properties.DOOR_OPENS);
            this.EventType = content.GetPropertyValue<string>(Constants.Recommendation.Properties.EVENT_TYPE);
            this.LinkToEvent = content.GetPropertyValue<string>(Constants.Recommendation.Properties.LINK_TO_EVENT);
            this.ImageUrl = content.GetCropUrl(Constants.Recommendation.Properties.IMAGE_URL, Constants.Crop.RECOMMENDATION_IMAGE);
            this.FacebookImageUrl = content.GetCropUrl(Constants.Recommendation.Properties.IMAGE_URL, Constants.Crop.FACEBOOK_IMAGE);
            this.TicketUrl = content.GetPropertyValue<string>(Constants.Recommendation.Properties.TICKET_URL);
            this.OldId = content.GetPropertyValue<int>(Constants.Recommendation.Properties.OLD_ID);

            if (this.IsGuestWriter == false)
            {
                int writerId = content.GetPropertyValue<int>(Constants.Recommendation.Properties.WRITER);
                this.WriterId = writerId;
                IPublishedContent writer = this.Helper.TypedContent(writerId);

                if (writer != null)
                {
                    this.WriterName = writer.Name;
                    this.WriterImage = writer.GetCropUrl(Constants.Writer.Properties.IMAGE, Constants.Crop.WRITER_IMAGE);
                    this.WriterBackground = writer.GetPropertyValue<string>(Constants.Writer.Properties.BACKGROUND);
                }             
            }

            int translatorId = content.GetPropertyValue<int>(Constants.Recommendation.Properties.TRANSLATOR);
            this.TranslatorId = translatorId;
            IPublishedContent translator = this.Helper.TypedContent(translatorId);

            if (translator != null)
            {
                this.TranslatorName = translator.Name;
            }

            int locationId = content.GetPropertyValue<int>(Constants.Recommendation.Properties.LOCATION);

            IPublishedContent location = this.Helper.TypedContent(locationId);

            if (location != null)
                this.Location = LocationFactory.Create(location, recommendationRepository, contentService, isEnglish, isDetails:false);
        }
    }
}
