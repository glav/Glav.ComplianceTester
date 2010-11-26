using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComplianceTester.Contracts;
using System.Net;
using System.IO;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;

namespace ComplianceTester.Model
{
    public class W3CComplianceProvider : ComplianceComponentBase, IComplianceValidationProvider
    {
        const string W3C_HTML_URL = "http://validator.w3.org/check";

        public IComplianceResult CheckCompliance(IComplianceDataSource dataSource, ComplianceLevel complianceLevel)
        {
            Contract.Assert(dataSource != null);

            var result = new ComplianceResult();

            var dataToCheck = dataSource.GetDataToCheck();
            if (string.IsNullOrWhiteSpace(dataToCheck))
            {
                result.ComplianceMessages.Add("Unkown error retrieving data for compliance checking.");
            }
            else
            {
                var errors = CheckDataForComplianceErrors(dataToCheck);
                if (errors.Count() == 0)
                    result.IsCompliant = true;
                else
                    result.ComplianceMessages.AddRange(errors);
            }


            return result;
        }

        string SendW3cRequest(WebHeaderCollection responseHeaders, string dataToCheck)
        {
            var values = new NameValueCollection();
            values.Add("fragment", dataToCheck);
            values.Add("prefill", "0");
            values.Add("group", "0");
            values.Add("doctype", "inline");

            string result;
            using (var wc = new WebClient())
            {
                try
                {
                    byte[] w3cRawResponse = wc.UploadValues(W3C_HTML_URL, values);
                    result = Encoding.UTF8.GetString(w3cRawResponse);
                    responseHeaders.Add(wc.ResponseHeaders);
                }
                catch (WebException we)
                {
                    we.Data.Add("url", W3C_HTML_URL);
                    throw;
                }
            }
            return result;
        }

        static IEnumerable<string> ExtractW3cResult(WebHeaderCollection w3cResponseHeaders)
        {
            List<string> messages = new List<string>();
            int warnings = 0;
            int errors = 0;

            if (int.TryParse(w3cResponseHeaders["X-W3C-Validator-Warnings"], out warnings) &&
                    int.TryParse(w3cResponseHeaders["X-W3C-Validator-Errors"], out errors))
            {

                if (warnings > 0)
                    messages.Add(string.Format("Warnings Found: {0}",warnings));
                if (errors > 0)
                    messages.Add(string.Format("Errors Found: {0}",errors));

                string status = w3cResponseHeaders["X-W3C-Validator-Status"];
                if (!String.IsNullOrEmpty(status))
                {
                    if (!status.Equals("Valid", StringComparison.InvariantCultureIgnoreCase))
                        messages.Add("Non compliant W3C HTML found.");
                }
                else
                {
                    messages.Add("HTML Fragment not parsed successfully.");
                }
            }
            else
            {
                messages.Add("Unableto Parse W3C Validity results successfully");
            }
            return messages;
        }



        private IEnumerable<string> CheckDataForComplianceErrors(string dataToCheck)
        {
            List<string> errors = new List<string>();
            var responseHeaders = new WebHeaderCollection();

            var result = SendW3cRequest(responseHeaders, dataToCheck);
            var messages = ExtractW3cResult(responseHeaders);
            if (messages != null && messages.Count() > 0)
                errors.AddRange(messages);

            return errors;
        }

    }
}
