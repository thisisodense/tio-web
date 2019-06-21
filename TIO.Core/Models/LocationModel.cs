using System;
using System.Collections.Generic;
using Ubolt.OpeningHours.Models;
using Umbraco.Core.Models;
using Umbraco.Web;
using System.Linq;
using Umbraco.Core.Services;
using Umbraco.Web.Models;

namespace TIO.Core.Models
{
    public abstract class LocationModel : RenderModel
    {
        public abstract string ShortDescriptionProperty { get; }
        public abstract string LongDescriptionProperty { get; }
        public string GeoLocation { get; private set; }
        public string Address { get; private set; }
        public string Title { get; private set; }
        public List<string> Categories { get; private set; }
        public List<string> Tags { get; private set; }
        public string WriterName { get; private set; }
        public int WriterId { get; set; }
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public OpeningHoursModel Openings { get; private set; }
        public string Image { get; private set; }
        public string FacebookImage { get; private set; }
        public string Url { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string HashTags { get; private set; }
        public int Id { get; private set; }
        public int OldId { get; private set; }
        public bool IsDetails { get; private set; }
        public List<KeyValuePair<string, string>> SimpleOpeningHours { get; set; }
        public bool PlacesToGoin2015 { get; private set; }
        public bool PlacesToGoIn2016 { get; private set; }
        public bool FromRecommendation { get; private set; }
        public int TimesRecommended { get; private set; }
        public bool ShowAsSimple { get { return (PlacesToGoin2015 || PlacesToGoIn2016 || FromRecommendation) == false; } }
        public string Fotograf { get; private set; }
        public string FotografURL { get; private set; }

        public string TranslatorName { get; private set; }
        public int TranslatorId { get; set; }
        public DateTime Created { private set; get; }

        public LocationModel(
            IPublishedContent content,
            IPublishedContent recommendationRepository,
            IContentService contentService,
            bool isDetails) 
            : base (content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            if (contentService == null)
                throw new ArgumentNullException("contentService");

            if (recommendationRepository == null)
                throw new ArgumentException("recommendationRepository");

            this.Tags = new List<string>();
            this.Categories = new List<string>();
            this.IsDetails = isDetails;
            this.GeoLocation = content.GetPropertyValue<string>(Constants.Location.Properties.GEO_LOCATION);
            this.Address = content.GetPropertyValue<string>(Constants.Location.Properties.ADDRESS);
            this.Openings = content.GetPropertyValue<OpeningHoursModel>(Constants.Location.Properties.OPENING_HOURS);
            this.Image = content.GetCropUrl(Constants.Location.Properties.IMAGE, Constants.Crop.LOCATION_IMAGE);
            this.FacebookImage = content.GetCropUrl(Constants.Location.Properties.IMAGE, Constants.Crop.FACEBOOK_IMAGE);
            this.Url = content.GetPropertyValue<string>(Constants.Location.Properties.URL);
            this.Phone = content.GetPropertyValue<string>(Constants.Location.Properties.PHONE_NUMBER);
            this.Email = content.GetPropertyValue<string>(Constants.Location.Properties.EMAIL);
            this.HashTags = content.GetPropertyValue<string>(Constants.Location.Properties.HASHTAGS);
            this.Title = content.GetPropertyValue<string>(Constants.Location.Properties.TITLE);
            this.WriterName = content.GetPropertyValue<string>(Constants.Location.Properties.WRITER);
            this.ShortDescription = content.GetPropertyValue<string>(this.ShortDescriptionProperty);
            this.LongDescription = content.GetPropertyValue<string>(this.LongDescriptionProperty);
            this.PlacesToGoin2015 = content.GetPropertyValue<bool>(Constants.Location.Properties.PLACES_TO_GO_IN_2015);
            this.PlacesToGoIn2016 = content.GetPropertyValue<bool>(Constants.Location.Properties.PLACES_TO_GO_IN_2016);
            this.FromRecommendation = content.GetPropertyValue<bool>(Constants.Location.Properties.FROM_RECOMMENDATION);
            this.Fotograf = content.GetPropertyValue<string>(Constants.Location.Properties.FOTOGRAF);
            this.FotografURL = content.GetPropertyValue<string>(Constants.Location.Properties.FOTOGRAF_URL);
            this.Created = content.CreateDate;

            this.Id = content.Id;
            this.OldId = content.GetPropertyValue<int>(Constants.Location.Properties.OLD_ID);

            this.TimesRecommended = recommendationRepository.Children(x => x.IsVisible() && x.GetPropertyValue<int>(Constants.Recommendation.Properties.LOCATION) == this.Id).Count();

            string categories = content.GetPropertyValue<string>(Constants.Location.Properties.CATEGORIES);

            if (categories != null)
                this.Categories.AddRange(categories.Split(','));

            string tags = content.GetPropertyValue<string>(Constants.Location.Properties.TAGS);

            if (tags != null)
                this.Tags.AddRange(tags.Split(','));

            IContent writer = contentService.GetById(content.GetPropertyValue<int>(Constants.Location.Properties.WRITER));

            if (writer != null)
            {
                this.WriterName = writer.GetValue<string>(Constants.Writer.Properties.NAME);
                this.WriterId = writer.Id;
            }

            IContent translator = contentService.GetById(content.GetPropertyValue<int>(Constants.Location.Properties.TRANSLATOR));

            if (translator != null)
            {
                this.TranslatorName = translator.GetValue<string>(Constants.Writer.Properties.NAME);
                this.TranslatorId = translator.Id;
            }

            UmbracoHelper helper = new UmbracoHelper(UmbracoContext.Current);

            if (this.Openings != null)
            {
                List<GroupOpeningHours> openingHours = new List<GroupOpeningHours>();

                openingHours.Add(new GroupOpeningHours(helper.GetDictionaryValue("Man"), this.Openings.Monday));
                openingHours.Add(new GroupOpeningHours(helper.GetDictionaryValue("Tir"), this.Openings.Tuesday));
                openingHours.Add(new GroupOpeningHours(helper.GetDictionaryValue("Ons"), this.Openings.Wednesday));
                openingHours.Add(new GroupOpeningHours(helper.GetDictionaryValue("Tor"), this.Openings.Thursday));
                openingHours.Add(new GroupOpeningHours(helper.GetDictionaryValue("Fre"), this.Openings.Friday));
                openingHours.Add(new GroupOpeningHours(helper.GetDictionaryValue("Lør"), this.Openings.Saturday));
                openingHours.Add(new GroupOpeningHours(helper.GetDictionaryValue("Søn"), this.Openings.Sunday));


                SimpleOpeningHours = new List<KeyValuePair<string, string>>();

                string fromDay = "";
                for (int i = 0; i < openingHours.Count; i++)
                {
                    var openingHour = openingHours[i];
                    string currentHours = string.Format("{0} - {1}", openingHour.Date.From, openingHour.Date.Till);

                    if ("-".Equals(currentHours.Trim()))
                    {
                        currentHours = helper.GetDictionaryValue("Lukket");
                    }

                    if (SimpleOpeningHours.Count > 0 && SimpleOpeningHours[SimpleOpeningHours.Count - 1].Value == currentHours)
                    {
                        SimpleOpeningHours.RemoveAt(SimpleOpeningHours.Count - 1);
                        SimpleOpeningHours.Add(new KeyValuePair<string, string>(string.Format("{0} - {1}", fromDay, openingHour.Day), currentHours));
                    }
                    else
                    {
                        fromDay = openingHour.Day;
                        SimpleOpeningHours.Add(new KeyValuePair<string, string>(fromDay, currentHours));
                    }
                }
            }
        }

            
    }

    public class GroupOpeningHours
    {
        public OpeningHourDateModel Date { get; private set; }
        public string Day { get; private set; }
        public GroupOpeningHours(
            string day,
            OpeningHourDateModel date)
        {
            this.Date = date;
            this.Day = day;
        }
    }
}
