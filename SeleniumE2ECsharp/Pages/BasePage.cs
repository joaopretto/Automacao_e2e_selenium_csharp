using OpenQA.Selenium;

namespace SeleniumE2ECsharp.Pages
{
    public class BasePage : TestBase.TestBase
    {
        public BasePage(IWebDriver driver) : base(driver) { }

        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }
    }
}