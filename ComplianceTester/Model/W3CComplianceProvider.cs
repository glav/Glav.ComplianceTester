using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplianceTester.Contracts;

namespace ComplianceTester.Model
{
    public class W3CComplianceProvider : IComplianceValidationProvider
    {
        public IComplianceResult CheckCompliance(IComplianceDataSource dataSource, ComplianceLevel complianceLevel)
        {
            throw new NotImplementedException();
        }
    }
}
