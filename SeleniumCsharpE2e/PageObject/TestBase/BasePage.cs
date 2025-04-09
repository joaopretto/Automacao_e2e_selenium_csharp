using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumCsharpE2e.Config;

namespace SeleniumCsharpE2e.PageObject
{
    public abstract class BasePage : HasWebDriverAccess
    {
        protected IWebDriver webdriver => base.WebDriver;

        public WebDriverWait Wait(int timeoutSeconds = 60)
        {
            return new WebDriverWait(base.WebDriver, TimeSpan.FromSeconds(timeoutSeconds));
        }
    }
}
