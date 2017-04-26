using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace TIO.Core.Models
{
    public abstract class ArticleModel : RenderModel
    {
        public abstract string SummaryProperty { get; }
        public abstract string HeadLineProperty { get; }
        public int Id { private set; get; }
        public string Headline { private set; get; }
        public int WriterId { private set; get; }
        public string WriterName { private set; get; }
        public string WeekNumber { private set; get; }
        public DateTime PublishDate { private set; get; }
        public List<string> Categories { private set; get; }
        public List<string> Tags { private set; get; }
        [AllowHtml]
        public string Summary { private set; get; }
        public int TranslatorId { private set; get; }
        public string TranslatorName { private set; get; }
        public string HeadlineEnglish { private set; get; }
        public string SummaryEnglish { private set; get; }
        public string Image { private set; get; }
        public string FacebookImage { private set; get; }
        public bool GuestWriter { private set; get; }
        public string NameOfGuestWriter { private set; get; }
        public string LinkToGuestWriter { private set; get; }
        public string Fotograf { private set; get; }
        public string FotografURL { private set; get; }
        public bool IsDetails { private set; get; }

        public ArticleModel(
            IPublishedContent content,
            IPublishedContent articleRepository,
            IContentService contentService,
            bool isDetails)
            : base(content)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            if (contentService == null)
                throw new ArgumentNullException("contentService");

            if (articleRepository == null)
                throw new ArgumentException("articleRepository");

            this.IsDetails = isDetails;
            this.Tags = new List<string>();
            this.Categories = new List<string>();
            this.Summary = content.GetPropertyValue<string>(this.SummaryProperty);
            this.Headline = content.GetPropertyValue<string>(this.HeadLineProperty);
            this.WeekNumber = content.GetPropertyValue<string>(Constants.Article.Properties.WEEKNUMBER);
            this.Fotograf = content.GetPropertyValue<string>(Constants.Article.Properties.fotograf);
            this.FotografURL = content.GetPropertyValue<string>(Constants.Article.Properties.fotografURL);
            this.GuestWriter = content.GetPropertyValue<bool>(Constants.Article.Properties.guestWriter);
            this.Image = content.GetCropUrl(Constants.Article.Properties.Image, Constants.Crop.RECOMMENDATION_IMAGE);
            this.FacebookImage = content.GetCropUrl(Constants.Article.Properties.Image, Constants.Crop.FACEBOOK_IMAGE);
            this.LinkToGuestWriter = content.GetPropertyValue<string>(Constants.Article.Properties.linkToGuestWriter);
            this.NameOfGuestWriter = content.GetPropertyValue<string>(Constants.Article.Properties.nameOfGuestWriter);
            this.PublishDate = content.GetPropertyValue<DateTime>(Constants.Article.Properties.PUBLISH_DATE);
            this.Id = content.Id;

            string categories = content.GetPropertyValue<string>(Constants.Article.Properties.CATEGORY_LONGREAD);

            if (categories != null)
                this.Categories.AddRange(categories.Split(','));

            string tags = content.GetPropertyValue<string>(Constants.Article.Properties.TAGS);

            if (tags != null)
                this.Tags.AddRange(tags.Split(','));

            IContent writer = contentService.GetById(content.GetPropertyValue<int>(Constants.Article.Properties.WRITER_LONGREAD));

            if (writer != null)
            {
                this.WriterName = writer.GetValue<string>(Constants.Writer.Properties.NAME);
                this.WriterId = writer.Id;
            }

            IContent translator = contentService.GetById(content.GetPropertyValue<int>(Constants.Article.Properties.translatorLongread));

            if (translator != null)
            {
                this.TranslatorName = translator.GetValue<string>(Constants.Writer.Properties.NAME);
                this.TranslatorId = translator.Id;
            }
        }
    }
}
