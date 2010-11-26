using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplianceTester.Model;

namespace ComplianceTester.Contracts
{
    public interface IComplianceValidationProvider
    {
        IComplianceResult CheckCompliance(IComplianceDataSource dataSource, ComplianceLevel complianceLevel);
    }
}
