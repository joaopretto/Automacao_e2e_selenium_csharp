using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumCsharpE2e.Config;

namespace SeleniumCsharpE2e.PageObject.TestBase
{
    public abstract class TestBase : HasWebDriverAccess
    {
        protected IWebDriver Driver;

        [SetUp]
        public void StartURL()
        {
            Driver = WebDriver;
        }

        [TearDown]
        public void Cleanup()
        {
            DisposeWebDriver();
        }
    }
}