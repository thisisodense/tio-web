using System;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Web.Routing;
using Umbraco.Web;
using Umbraco.Core.Models;
using TIO.Core.Models;
using System.Web.Http;

namespace TIO.Core
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var root = new UmbracoHelper(UmbracoContext.Current).TypedContentAtRoot().FirstOrDefault();

            routes.MapUmbracoRoute(
               name: "writer",
               url: "writer/{id}/{name}",
               defaults: new
               {
                   controller = "Writer",
                   action = "Writer",
                   name = UrlParameter.Optional
               },
               constraints: new { id = @"\d+" },
               virtualNodeHandler: new RecommendationNodeRouteHandler()
           );

            routes.MapUmbracoRoute(
                name: "about",
                url: "{lang}/about",
                defaults: new
                {
                    lang  = "da",
                    controller = "About",
                    action = "About"
                },
                constraints: new { lang = "[a-z]{2}" },
                virtualNodeHandler: new UmbracoVirtualNodeByIdRouteHandler(root != null ? root.Id : 0)
            );

            routes.MapUmbracoRoute(
               name: "enDefaultLocations",
               url: "{lang}/places/{tagId}/{tag}",
               defaults: new
               {
                   lang = "da",
                   tagId = UrlParameter.Optional,
                   tag = UrlParameter.Optional,
                   controller = "locations",
                   action = "locations"
               },
               constraints: new { lang = "[a-z]{2}" },
               virtualNodeHandler: new UmbracoVirtualNodeByIdRouteHandler(root != null ? root.Id : 0)
           );

            routes.MapUmbracoRoute(
                name: "enLocationDetails",
                url: "{lang}/place/{id}/{name}",
                defaults: new
                {
                    controller = "Location",
                    action = "Location",
                    lang = "da",
                    name = UrlParameter.Optional
                },
                constraints: new { lang = "[a-z]{2}", id = @"\d+" },
                virtualNodeHandler: new RecommendationNodeRouteHandler()
            );
            
            routes.MapUmbracoRoute(
               name: "locationDetails",
               url: "place/{id}/{name}",
               defaults: new
               {
                   controller = "Location",
                   action = "Location",
                   lang = "da",
                   name = UrlParameter.Optional
               },
               constraints: new { lang = "[a-z]{2}", id = @"\d+" },
               virtualNodeHandler: new RecommendationNodeRouteHandler()
           );

            routes.MapUmbracoRoute(
                name: "enDetails",
                url: "{lang}/{id}/{name}",
                defaults: new
                {
                    controller = "Recommendation",
                    action = "Recommendation",
                    lang = "da",
                    name = UrlParameter.Optional
                },
                constraints: new { lang = "[a-z]{2}", id = @"\d+" },
                virtualNodeHandler: new RecommendationNodeRouteHandler()
            );

            routes.MapUmbracoRoute(
               name: "details",
               url: "{id}/{name}",
               defaults: new
               {
                   controller = "Recommendation",
                   action = "Recommendation",
                   lang = "da",
                   name = UrlParameter.Optional
               },
               constraints: new { lang = "[a-z]{2}", id = @"\d+" },
               virtualNodeHandler: new RecommendationNodeRouteHandler()
           );

            routes.MapRoute(
                name: "tips",
                url: "tips",
                defaults: new {
                    controller = "Recommendations",
                    action = "Tips"
                });

            routes.MapUmbracoRoute(
                name: "default",
                url: "{lang}/{controller}/{action}",
                defaults: new { lang = "da", controller = "Recommendations", action = "Recommendations" },
                constraints: new { lang = "[a-z]{2}" },
                virtualNodeHandler: new UmbracoVirtualNodeByIdRouteHandler(root != null ? root.Id : 0) 
            );

            routes.MapUmbracoRoute(
               name: "preview",
               url: "{id}.aspx",
               defaults: new
               {
                   controller = "Recommendation",
                   action = "Recommendation",
                   lang = "da",
                   name = UrlParameter.Optional
               },
               constraints: new { id = @"\d+" },
               virtualNodeHandler: new RecommendationNodeRouteHandler()
            );

            routes.MapRoute(
                name: "defaultMVC",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapHttpRoute(
                name: "DictionanyApi",
                routeTemplate: "api/{controller}/{action}/{label}/{lang}"
            );
        }

        /// <summary>
        /// The generic login/register page node route handler.
        /// </summary>
        public class RecommendationNodeRouteHandler : UmbracoVirtualNodeRouteHandler
        {
            /// <summary>
            /// returns the <see cref="IPublishedContent"/> associated with the route.
            /// </summary>
            /// <param name="requestContext">
            /// The request context.
            /// </param>
            /// <param name="umbracoContext">
            /// The umbraco context.
            /// </param>
            /// <returns>
            /// The <see cref="IPublishedContent"/>.
            /// </returns>
            protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
            {
                UmbracoHelper helper = new UmbracoHelper(umbracoContext);

                int id = Convert.ToInt32(requestContext.RouteData.GetRequiredString("id"));

                var root = helper.TypedContentAtRoot().FirstOrDefault();

                if (root == null)
                    return helper.TypedContent(id);

                IPublishedContent content = root
                            .FirstChild(x => x.DocumentTypeAlias == Constants.NodeAlias.RECOMMENDATIONS_REPOSITORY)
                            .Children(x => x.IsVisible() && x.GetPropertyValue<int>(Constants.Recommendation.Properties.OLD_ID) == id)
                            .FirstOrDefault();

                if (content != null)
                    return content;

                return helper.TypedContent(id);
            }
        }
    }
}
