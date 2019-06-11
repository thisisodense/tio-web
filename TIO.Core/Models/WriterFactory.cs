using Umbraco.Core.Models;
using Umbraco.Core.Services;
using FILTER = TIO.Core.Models.Constants.Controllers.WriterArchive.FILTER;

namespace TIO.Core.Models
{
    public class WriterFactory
    {
        public static WriterModel Create(
            IPublishedContent content, 
            IPublishedContent recommendationsRespository, 
            IPublishedContent articleRespository,
            IPublishedContent locationRespository,
            bool isEnglish,
            bool isDetails,
            IContentService contentService, 
            FILTER filter, 
            int page)
        {
            return new WriterModel(
                content, 
                recommendationsRespository, 
                articleRespository, 
                locationRespository, 
                isEnglish, 
                isDetails, 
                contentService, 
                filter,
                page);
        }
    }
}
