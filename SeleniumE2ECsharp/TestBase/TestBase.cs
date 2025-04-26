using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumE2ECsharp.TestBase
{
    public class TestBase
    {
        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }

        public TestBase(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        protected void WaitForElement(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }
    }
}