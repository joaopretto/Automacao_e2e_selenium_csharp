using OpenQA.Selenium;
using SeleniumCsharpE2e.Utils.WebElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharpE2e.Utils.WebElement
{
    class ShadowElement : E2EElement
    {
        public ShadowElement(ShadowRoot shadowRoot, By path) : base(path, null)
        {
            Element = shadowRoot.Element.FindElement(path);
        }
    }
}
