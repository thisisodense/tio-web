using System.Collections.Generic;
using System.Linq;
using TIO.Core.Models;
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
        public ArticlesModel(
            IPublishedContent content,
            IPublishedContent articleRepository,
            bool isEnglish,
            int? tagId,
            string tag,
            IContentService contentService) :
            base(content)
        {
            UmbracoHelper helper = new UmbracoHelper(UmbracoContext.Current, content);

            this.Tags = helper.TagQuery.GetAllTags(Constants.NodeAlias.ARTICLE_TAG_GROUP);

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

            this.Articles
                .AddRange(articles.Select(x => ActicleFactory.Create(x, articleRepository, contentService, isEnglish, isDetails: false)));

            this.Articles = this.Articles.OrderByDescending(x => x.PublishDate).ToList();
        }
    }
}
