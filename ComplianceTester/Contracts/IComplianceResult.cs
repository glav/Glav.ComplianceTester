using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplianceTester.Contracts
{
    public interface IComplianceResult
    {
        bool IsCompliant { get;  }
        IEnumerable<string> ComplianceMessages { get;  }
    }
}
