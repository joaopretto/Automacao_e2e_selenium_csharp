using OpenQA.Selenium;
using SeleniumCsharpE2e.Utils.WebElement;

namespace SeleniumCsharpE2e.PageObject
{
    public class DemoQaObject : BasePage
    {
        
        public bool GetUrl(string url)
        {
            return webdriver.Url.Contains(url);
        }
    }
}