using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools
{
    public class FacebookHelper
    {
        public static async void Rescrape(string url)
        {
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("access_token", "550180138336430|2yUeUaRj_GqDcCv86mhn0iZVnBg"),
                new KeyValuePair<string, string>("scrape", "true"),
                new KeyValuePair<string, string>("id", url)
            });

            var response = await client.PostAsync("https://graph.facebook.com", content);

            var responseString = await response.Content.ReadAsStringAsync();

            Trace.TraceInformation(url);
            Trace.TraceInformation(responseString);
        }
    }
}
