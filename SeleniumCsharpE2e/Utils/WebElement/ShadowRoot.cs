using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpE2e.Utils.WebElement
{
    class ShadowRoot : E2EElement
    {
        public ShadowRoot(By path) : base(path)
        {
            Element = (IWebElement)((IJavaScriptExecutor)webdriver).ExecuteScript("return arguments[0].shadowRoot", Element);
        }

        public ShadowRoot(ShadowRoot shadowRoot, By path) : base(path, null)
        {
            Element = shadowRoot.Element.FindElement(path);
            Element = (IWebElement)((IJavaScriptExecutor)webdriver).ExecuteScript("return arguments[0].shadowRoot", Element);
        }
    }
}
