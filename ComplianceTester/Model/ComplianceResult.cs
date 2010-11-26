using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplianceTester.Contracts;

namespace ComplianceTester.Model
{
    internal class ComplianceResult : IComplianceResult
    {
        public bool IsCompliant {get; set;}

        List<string> _complianceMessages = new List<string>();
        public List<string> ComplianceMessages
        {
            get { return _complianceMessages; }
        }
    }
}
