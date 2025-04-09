using OpenQA.Selenium;
using System.Collections.Generic;

namespace SeleniumCsharpE2e.Utils.WebElement
{
    public class WebElementList : List<E2EElement>
    {
        //Default Constructor
        public WebElementList(By path)
        {
            foreach (IWebElement x in new E2EElement(path).WebDriver.FindElements(path))
            {
                Add(new E2EElement(path, x));
            }
        }
    }
}