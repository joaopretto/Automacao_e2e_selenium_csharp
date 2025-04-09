using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SeleniumCsharpE2e.Config.ConfigObjects
{
    public class TestCaseObject
    {
        private static List<TestCaseObject> TestCaseList = new List<TestCaseObject>();
        internal string TestCaseName { get; set; }
        internal int RetryCount { get; set; }
        internal HttpClient Driver { get; set; }
        public static TestCaseObject CurrentTestCase => GetCurrentTestCase();

        private static TestCaseObject GetCurrentTestCase()
        {
            var testCaseObject = TestCaseList.FirstOrDefault(l => l.TestCaseName == TestExecutionContext.CurrentContext.CurrentTest.FullName);

            if (testCaseObject == null)
            {
                testCaseObject = new TestCaseObject
                {
                    RetryCount = 1,
                    TestCaseName = TestExecutionContext.CurrentContext.CurrentTest.FullName
                };
                TestCaseList.Add(testCaseObject);
            }

            return testCaseObject;
        }
    }
}
