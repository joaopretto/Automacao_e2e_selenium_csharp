using OpenQA.Selenium;
using SeleniumE2ECsharp.Config;

namespace SeleniumE2ECsharp.Utils.WebElement
{
   public class E2EElement
   {
      private readonly By by;
      private IWebDriver Driver => DriverContext.Current;

      public E2EElement(By path)
      {
         by = path;
      }

      private IWebElement Element => Driver.FindElement(by);

      public void Click()
      {
         ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", Element);
         Element.Click();
      }
   }
}