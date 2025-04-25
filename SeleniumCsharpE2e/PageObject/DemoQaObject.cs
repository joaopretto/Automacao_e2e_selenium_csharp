using OpenQA.Selenium;
using SeleniumCsharpE2e.Utils.WebElement;

namespace SeleniumCsharpE2e.PageObject
{
    public class DemoQaObject : BasePage
    {

        E2EElement elementsCard = new E2EElement(By.CssSelector("[class='card mt-4 top-card']:first-child"));
        
        public bool GetUrl(string url)
        {
            return webdriver.Url.Contains(url);
        }

        public void ClickOnElementsCard()
        {
            elementsCard.Click();
        }
    }
}