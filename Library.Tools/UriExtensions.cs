using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools
{
    public static class UriExtensions
    {
        public static string TopLevelDomainname(this Uri url)
        {
            var host = url.Host.ToLower();

            string[] col = { ".com", ".cn", ".co.uk", ".dk", ".net", ".hdk" };

            foreach (string name in col)
            {
                if (host.EndsWith(name))
                {
                    int idx = host.IndexOf(name);
                    int sec = host.Substring(0, idx - 1).LastIndexOf('.');
                    return host.Substring(sec + 1);
                }
            }

            throw new ArgumentException("Couldnt find toplevel domain, try adding more top level domains to the list");
        }
    }
}
