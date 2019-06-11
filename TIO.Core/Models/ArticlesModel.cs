using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace TIO.Core.Models
{
    public class ArticlesModel : RenderModel
    {
        public List<ArticleModel> Articles { get; private set; }
        public IEnumerable<TagModel> Tags { get; private set; }
        public int Page { get; private set; }
        public int Total { get; private set; }
        public bool DisablePreviousButton { get; private set; }
        public bool DisableNextButton { get; private set; }
        private int pageSize = 5;
        public ArticlesModel(
            IPublishedContent content,
            IPublishedContent articleRepository,
            bool isEnglish,
            int? tagId,
            string tag,
            int page,
            IContentService contentService) :
            base(content)
        {
            UmbracoHelper helper = new UmbracoHelper(UmbracoContext.Current, content);

            this.Tags = helper.TagQuery.GetAllTags(Constants.NodeAlias.ARTICLE_TAG_GROUP);
            this.Page = page;

            if (tagId.HasValue)
            {
                tag = this.Tags.First(x => x.Id == tagId.Value).Text;
            }
            else
            {
                tag = Constants.NodeAlias.ARTICLE_DEFAULT_TAG;
            }


            this.Articles = new List<ArticleModel>();

            var articles = helper.TagQuery.GetContentByTag(tag, Constants.NodeAlias.ARTICLE_TAG_GROUP);

            this.Total = articles.Count();

            this.DisablePreviousButton = page == 1 ? true : false;
            this.DisableNextButton = page * pageSize > this.Total ? true : false;

            this.Articles
                .AddRange(articles
                    .Select(x => ActicleFactory.Create(x, articleRepository, contentService, isEnglish, isDetails: false))
                    .OrderByDescending(x => x.PublishDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize));

            this.Articles = this.Articles.ToList();
        }
    }
}
