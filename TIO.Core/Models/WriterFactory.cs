using Umbraco.Core.Models;

namespace TIO.Core.Models
{
    public class WriterFactory
    {
        public static WriterModel Create(
            IPublishedContent content, 
            IPublishedContent recommendationsRespository)
        {
            return new WriterModel(content, recommendationsRespository);
        }
    }
}
