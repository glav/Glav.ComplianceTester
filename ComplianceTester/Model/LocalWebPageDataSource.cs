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
    public class LocalWebPageDataSource : ComplianceComponentBase, IComplianceDataSource
    {
        private string _pageUri;

        public LocalWebPageDataSource(string pageUri)
        {
            _pageUri = pageUri;
        }

        public string GetDataToCheck()
        {
            return GetResponseBody(_pageUri);
        }
    }
}
