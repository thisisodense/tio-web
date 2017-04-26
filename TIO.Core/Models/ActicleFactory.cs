using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TIO.Core.Models
{
    public class ActicleFactory
    {
        public static ArticleModel Create(
            IPublishedContent content,
            IPublishedContent articleRepository,
            IContentService contentService,
            bool isEnglish,
            bool isDetails)
        {
            if (isEnglish)
                return new ArticleUKModel(content, articleRepository, contentService, isDetails);

            return new ArticleDKModel(content, articleRepository, contentService, isDetails);
        }
    }
}
