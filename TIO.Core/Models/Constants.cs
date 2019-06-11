namespace TIO.Core.Models
{
    public class Constants
    {
        public class NodeAlias
        {
            public const string RECOMMENDATIONS_REPOSITORY = "RecommendationRepository";
            public const string RECOMMENDATION = "Recommendation";
            public const string WRITER = "Writer";
            public const string WRITER_REPOSITORY = "WriterRepository";
            public const string LOCATION_REPOSITORY = "LocationRepository";
            public const string LOCATION = "Location";
            public const string ARTICLE = "Article";
            public const string ARTICLE_REPOSISTORY = "ArticleRepository";
            public const string ARTICLE_TAG_GROUP = "WebTagsLongread";
            public const string ARTICLE_DEFAULT_TAG = "Articles";
            public const string TAG_GROUP = "WebTags";
            public const string DEFAULT_TAG = "Top Picks";
        }

        public class CustomDataTypes
        {
            public const string EVENT_TYPES = "Dropdown event types";
            public const string CATEGROIES = "Dropdown Location Categories";
        }
        public class Recommendations
        {
            public class Properties
            {
                public const string EDITOR_OF_WEEK = "editorOfTheWeek";
                public const string WEEK_DESCRIPTION = "weekDescriptionDA";
                public const string WEEK_DESCRIPTION_EN = "weekDescriptionEN";
            }
        }

        public class Recommendation
        {
            public class Properties
            {
                public const string START_DATE = "startDate";
                public const string END_DATE = "endDate";
                public const string HEADLINE = "headline";
                public const string SUB_HEADER = "subHeader";
                public const string GUEST_WRITER_NAME = "nameOfGuestWriter";
                public const string GUEST_WRITER_LINK = "linkToGuestWriter";
                public const string GUEST_WRITER = "guestWriter";
                public const string WRITER_NAME = "writerName";
                public const string HEADLINE_UK = "headlineEnglish";
                public const string SUB_HEADER_UK = "subheaderEnglish";
                public const string BODY = "summary";
                public const string BODY_UK = "summaryEnglish";
                public const string WRITER = "writerRecommendation";
                public const string LOCATION = "locationRecommendation";
                public const string ORGANIZER = "organizer";
                public const string PRICE = "price";
                public const string START_TIME = "startTime";
                public const string END_TIME = "endTime";
                public const string DOOR_OPENS = "doorsOpen";
                public const string EVENT_TYPE = "eventType";
                public const string TICKET_URL = "ticketUrl";
                public const string IMAGE_URL = "image";
                public const string LINK_TO_EVENT = "linkToEvent";
                public const string OLD_ID = "oldId";
                public const string OLD_IMAGE = "oldImage";
                public const string WEEK = "weekNumber";
                public const string TRANSLATOR = "translatorRecommendation";
            }
        }

        public class Writer
        {
            public class Properties
            {
                public const string OLD_ID = "oldId";
                public const string NAME = "name";
                public const string EMAIL = "email";
                public const string BACKGROUND = "background";
                public const string INTERESTS = "interests";
                public const string IMAGE = "image";
                public const string SHOW = "show";
                public const string TITLE = "title";
                public const string ABOUT = "about";
                public const string ID = "id";
                public const string WRITER_VIDEO = "writervideo";
                public const string LINK = "personligtlink";
                public const string WRITER_SINCE = "writersince";
            }
        }

        public class Location
        {
            public class Properties
            {
                public const string GEO_LOCATION = "geolocation";
                public const string ADDRESS = "address";
                public const string TITLE = "title";
                public const string TAGS = "tags";
                public const string CATEGORIES = "categories";
                public const string WRITER = "writerLocation";
                public const string SHORT_DESCRIPTION = "shortdescription";
                public const string SHORT_DESCRIPTION_EN = "shortdescriptionenglish";
                public const string LONG_DESCRIPTION = "longdescription";
                public const string LONG_DESCRIPTION_EN = "longdescriptionenglish";
                public const string OPENING_HOURS = "openinghours";
                public const string IMAGE = "image";
                public const string URL = "url";
                public const string PHONE_NUMBER = "phonenumber";
                public const string EMAIL = "email";
                public const string HASHTAGS = "hashtags";
                public const string OLD_ID = "oldid";
                public const string PLACES_TO_GO_IN_2015 = "PlacesToGoin2015";
                public const string PLACES_TO_GO_IN_2016 = "PlacesToGoin2016";
                public const string FROM_RECOMMENDATION = "fromRecommendation";
                public const string FOTOGRAF = "fotograf";
                public const string FOTOGRAF_URL = "fotografURL";
                public const string TRANSLATOR = "translatorLocation";
            }
        }

        public class Article
        {
            public class Properties
            {
                public const string HEADLINE = "headline";
                public const string WRITER_LONGREAD = "writerLongread";
                public const string WEEKNUMBER = "weekNumber";
                public const string PUBLISH_DATE = "udgivelsesdato";
                public const string CATEGORY_LONGREAD = "categoriesLongread";
                public const string TAGS = "tags";
                public const string SUMMARY = "summary";
                public const string klarTilKorrektur = "klarTilKorrektur";
                public const string klarTilOversaettelse = "klarTilOversaettelse";
                public const string translatorLongread = "translatorLongread";
                public const string headlineEnglish = "headlineEnglish";
                public const string summaryEnglish = "summaryEnglish";
                public const string Image = "Image";
                public const string guestWriter = "guestWriter";
                public const string nameOfGuestWriter = "nameOfGuestWriter";
                public const string linkToGuestWriter = "linkToGuestWriter";
                public const string fotograf = "fotograf";
                public const string fotografURL = "fotografURL";

            }
        }
        
        public class Crop
        {
            public const string RECOMMENDATION_IMAGE = "recommendationCrop";
            public const string WRITER_IMAGE = "writerCrop";
            public const string LOCATION_IMAGE = "locationCrop";
            public const string FACEBOOK_IMAGE = "facebookCrop";
            public const string MINITURE_CROP = "miniatureCrop";
        } 

        public class Js
        {
            public const string General = "~/js/general";
            public const string Jquery = "~/js/jquery";
            public const string Moderinzr = "~/js/modernizr";
            public const string JqueryUI = "~/js/jqueryui";
            public const string Locations = "~/js/locations";
        }

        public class Css
        {
            public const string General = "~/css/general";
            public const string Locations = "~/css/locations";
        }

        public class Controllers
        {
            public class Recommendation
            {
                public const string NAME = "Recommendation";

                public class Actions
                {
                    public const string RECOMMENDATION = "Recommendation";
                }
            }

            public class Location
            {
                public const string NAME = "Location";

                public class Actions
                {
                    public const string LOCATION = "Location";
                }
            }

            public class Writer
            {
                public const string NAME = "Writer";
                public class Actions
                {
                    public const string WRITER = "Writer";
                }
            }

            public class WriterArchive
            {
                public const string NAME = "WriterArchive";
                public enum FILTER { Recommendations, Articles, Locations}
                public class Actions
                {
                    public const string INDEX = "WriterArchive";
                }
            }

            public class Article
            {
                public const string NAME = "Article";

                public class Actions
                {
                    public const string ARTICLE = "Article";
                }
            }

            public class Articles
            {
                public const string NAME = "Articles";
                public class Actions
                {
                    public const string ARTICLES = "Articles";
                }
            }
        }
    }
}
