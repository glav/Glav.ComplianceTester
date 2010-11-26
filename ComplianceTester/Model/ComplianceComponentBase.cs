using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplianceTester.Contracts;
using System.Net;
using System.IO;

namespace ComplianceTester.Model
{
    public abstract class ComplianceComponentBase : IComplianceComponent
    {
        const string W3C_HTML_URL = "http://validator.w3.org/check";
        const string FIREFOX_USER_AGENT = "Mozilla/5.0 (Windows; U; Windows NT 6.0; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1 (.NET CLR 3.5.30729)";
        const string ACCEPT_HEADER = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
        const string ACCEPT_LANGUAGE = "en-au,en-us;q=0.7,en;q=0.3";

        public string GetResponseBody(string url)
        {
            WebClient client = new WebClient();
            // Pretend to be Firefox 3 so that ASP.NET renders compliant HTML
            client.Headers["User-Agent"] = FIREFOX_USER_AGENT;
            client.Headers["Accept"] = ACCEPT_HEADER;
            client.Headers["Accept-Language"] = ACCEPT_LANGUAGE;

            string body;
            using (Stream responseStream = client.OpenRead(url))
            {
                using (var sr = new StreamReader(responseStream))
                {
                    body = sr.ReadToEnd();
                    sr.Close();
                }
            }

            return body;
        }
    }
}
