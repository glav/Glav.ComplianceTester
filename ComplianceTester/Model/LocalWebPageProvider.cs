using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplianceTester.Contracts;
using System.Web;
using System.Net;
using System.IO;

namespace ComplianceTester.Model
{
    public class LocalWebPageProvider : IComplianceDataSource
    {
        private string _pageUri;

        public LocalWebPageProvider(string pageUri)
        {
            _pageUri = pageUri;
        }

        public string GetDataToCheck()
        {
            WebClient client = new WebClient();
            using (var stream = client.OpenRead(_pageUri))
            {
                using (var rdr = new StreamReader(stream))
                {
                    return rdr.ReadToEnd();
                }
            }
        }
    }
}
