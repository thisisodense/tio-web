using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Library.Tools
{
    public static class StringExtensions
    {
        private static string RemoveAccent(string txt)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }


        public static string ToSeoUrl(this string url)
        {
            // make the url lowercase
            string encodedUrl = (url ?? "").ToLower();

            Dictionary<string, string> replace = new Dictionary<string, string>() 
            { 
                { "å", "aa" },
                { "æ", "ae" },
                { "ø", "oe" }
            };

            encodedUrl = replace.Aggregate(encodedUrl,
                                             (current, replacement) =>
                                             current.Replace(replacement.Key, replacement.Value));

            //Remove the accents from text
            encodedUrl = RemoveAccent(encodedUrl);

            // invalid chars           
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            encodedUrl = Regex.Replace(encodedUrl, @"\s+", "-").Trim();

            //replace multiple "-"'s into one.
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            //Trim starting and ending "-"'s
            return encodedUrl.Trim('-');
        }
        
    }
}
