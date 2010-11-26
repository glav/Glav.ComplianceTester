using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplianceTester.Contracts
{
    public interface IComplianceComponent
    {
        string GetResponseBody(string url);
    }
}
