using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplianceTester.Contracts
{
    public interface IComplianceDataSource
    {
        string GetDataToCheck();
    }
}
