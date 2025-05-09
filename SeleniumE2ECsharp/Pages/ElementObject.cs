using OpenQA.Selenium;
using SeleniumE2ECsharp.Config;
using SeleniumE2ECsharp.Utils.WebElement;

namespace SeleniumE2ECsharp.Pages
{
    public class ElementObject
    {
        public E2EElement elementCard => new E2EElement(By.XPath("//*[@id='app']/div/div/div[2]/div/div[1]/div/div[3]"));

        public void ClickOnElementCard()
        {
            elementCard.Click();
        }
    }
}