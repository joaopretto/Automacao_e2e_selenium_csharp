using SeleniumCsharpE2e.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using SeleniumCsharpE2e.Utils.WebElement;
using SeleniumCsharpE2e.PageObject;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCsharpE2e.Utils.WebElement
{
    public class E2EElement : BasePage
    {
        object numberOfConnections(IJavaScriptExecutor webdriver) => webdriver.ExecuteScript("return window.openHTTPs");
        public By Path { get; set; }
        public string Text => Element.Text;
        public bool Displayed => Element.Displayed;
        public IWebElement Element { get; set; }

        //Default Constructor/Used in tests
        public E2EElement(By path)
        {
            Path = path;
            Element = FindElement();
        }

        //List Constructor, normally not used in tests
        public E2EElement(By path, IWebElement element)
        {
            Path = path;
            Element = element;
        }

        //Default click action
        public void Click()
        {
            ((IJavaScriptExecutor)webdriver).ExecuteScript("arguments[0].scrollIntoView(false);", Element);
            Element.Click();
        }

        public void SendKeysFocused(string keys, bool clear = false)
        {
            if (string.IsNullOrEmpty(keys)) return;
            Actions act = new Actions(webdriver);

            if (clear)
            {
                act.Click(Element)
                   .KeyDown(Keys.Control)
                   .SendKeys("a")
                   .KeyUp(Keys.Control)
                   .SendKeys(keys)
                   .Perform();

            }
            else
            {
                act.Click(Element)
                   .SendKeys(keys)
                   .Perform();
            }
        }

        public void SelectValue(string value)
        {
            Click();
            foreach (IWebElement x in Element.FindElements(By.CssSelector("span[class]")))
            {
                if (x.Text.Contains(value))
                {
                    x.Click();
                    break;
                }
            }
        }

        public void DoubleClick()
        {
            new Actions(webdriver).DoubleClick(Element).Perform();
        }

        //Clear text from elementE
        public void Clear()
        {
            Element.Clear();
        }

        //Writes text in an element
        public void SendKeys(string keys, bool clear = true)
        {
            if (string.IsNullOrEmpty(keys)) return;
            if (clear)
            {
                try
                {
                    Element.Clear();
                }
                catch
                {

                }
            }
            Element.SendKeys(keys);
        }

        //Returns X attribute from an element
        public string GetAttribute(string attribute)
        {
            return Element.GetAttribute(attribute);
        }

        //Returns X property from an element
        public string GetProperty(string property)
        {
            return Element.GetDomProperty(property);
        }

        //Wait until element is visible, returns a boolean
        public void WaitUntilIsPresent()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(webdriver, new TimeSpan(0, 0, 60));
                wait.Until(ExpectedConditions.ElementExists(Path));
            }
            catch
            {
                throw new WebDriverTimeoutException("Could not find an element by " + Path.ToString() + " after 60 seconds");
            }

        }

        //Wait until page is loaded
        public E2EElement WaitForNetwork(int timeout = 0)
        {
            IJavaScriptExecutor jsExec = (IJavaScriptExecutor)webdriver;
            WebDriverWait jsWait = new WebDriverWait(webdriver, new TimeSpan(0, 0, 60));

            waitUntilJSReady(jsExec, jsWait);
            ajaxComplete(jsExec);
            waitUntilJQueryReady(jsExec, jsWait);
            waitUntilAngularReady(jsExec, jsWait);
            //waitUntilAngular5Ready();
            WaitForHttpRequests(jsExec);

            return this;
        }

        void ajaxComplete(IJavaScriptExecutor jsExec)
        {
            jsExec.ExecuteScript("var callback = arguments[arguments.length - 1];"
                + "var xhr = new XMLHttpRequest();" + "xhr.open('GET', '/Ajax_call', true);"
                + "xhr.onreadystatechange = function() {" + "  if (xhr.readyState == 4) {"
                + "    callback(xhr.responseText);" + "  }" + "};" + "xhr.send();");
        }

        void waitForJQueryLoad(IJavaScriptExecutor jsExec, WebDriverWait jsWait)
        {
            try
            {
                Func<IWebDriver, bool> jQueryLoad = driver => ((long)((IJavaScriptExecutor)driver).ExecuteScript("return jQuery.active") == 0);

                bool jqueryReady = (bool)jsExec.ExecuteScript("return jQuery.active==0");

                if (!jqueryReady)
                {
                    jsWait.Until(jQueryLoad);
                }
            }
            catch (WebDriverException)
            {
            }
        }

        void waitForAngularLoad(IJavaScriptExecutor jsExec, WebDriverWait jsWait)
        {
            string angularReadyScript = "return angular.element(document).injector().get('$http').pendingRequests.length === 0";
            angularLoads(angularReadyScript, jsExec, jsWait);
        }

        void waitUntilJSReady(IJavaScriptExecutor jsExec, WebDriverWait jsWait)
        {
            try
            {
                Func<IWebDriver, bool> jsLoad = driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString().Equals("complete");

                bool jsReady = jsExec.ExecuteScript("return document.readyState").ToString().Equals("complete");

                if (!jsReady)
                {
                    jsWait.Until(jsLoad);
                }
            }
            catch (WebDriverException)
            {
                return;
            }
        }

        void waitUntilJQueryReady(IJavaScriptExecutor jsExec, WebDriverWait jsWait)
        {
            bool jQueryDefined = (bool)jsExec.ExecuteScript("return typeof jQuery != 'undefined'");
            if (jQueryDefined)
            {
                waitForJQueryLoad(jsExec, jsWait);
            }
        }

        void waitUntilAngularReady(IJavaScriptExecutor jsExec, WebDriverWait jsWait)
        {
            try
            {
                bool angularUnDefined = (bool)jsExec.ExecuteScript("return window.angular === undefined");
                if (!angularUnDefined)
                {
                    bool angularInjectorUnDefined = (bool)jsExec.ExecuteScript("return angular.element(document).injector() === undefined");
                    if (!angularInjectorUnDefined)
                    {
                        waitForAngularLoad(jsExec, jsWait);
                    }
                }
            }
            catch (WebDriverException)
            {
                return;
            }
        }

        void waitUntilAngular5Ready(IJavaScriptExecutor jsExec, WebDriverWait jsWait)
        {
            try
            {
                object angular5Check = jsExec.ExecuteScript("return getAllAngularRootElements()[0].attributes['ng-version']");
                if (angular5Check != null)
                {
                    bool angularPageLoaded = (bool)jsExec.ExecuteScript("return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1");
                    if (!angularPageLoaded)
                    {
                        waitForAngular5Load(jsExec, jsWait);
                    }
                }
            }
            catch (WebDriverException)
            {
                return;
            }
        }

        void waitForAngular5Load(IJavaScriptExecutor jsExec, WebDriverWait jsWait)
        {
            string angularReadyScript = "return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1";
            angularLoads(angularReadyScript, jsExec, jsWait);
        }

        void angularLoads(string angularReadyScript, IJavaScriptExecutor jsExec, WebDriverWait jsWait)
        {
            try
            {
                Func<IWebDriver, bool> angularLoad = driver => bool.Parse(((IJavaScriptExecutor)driver).ExecuteScript(angularReadyScript).ToString());

                bool angularReady = bool.Parse(jsExec.ExecuteScript(angularReadyScript).ToString());

                if (!angularReady)
                {
                    jsWait.Until(angularLoad);
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        void WaitForHttpRequests(IJavaScriptExecutor jsExec)
        {

            if (numberOfConnections(jsExec) == null)
            {
                MonkeyPatchXMLHttpRequest(jsExec);
            }
            else if (numberOfConnections(jsExec).GetType() != typeof(long))
            {
                MonkeyPatchXMLHttpRequest(jsExec);
            }

            for (int index = 0; index < 300; index++)
            {
                try
                {
                    if ((long)numberOfConnections(jsExec) == 0L)
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }

                Thread.Sleep(20);
            }
        }

        void MonkeyPatchXMLHttpRequest(IJavaScriptExecutor webdriver)
        {
            try
            {
                IJavaScriptExecutor jsDriver = webdriver;
                string script = "  (function() {" +
                    "var oldOpen = XMLHttpRequest.prototype.open;" +
                    "window.openHTTPs = 0;" +
                    "XMLHttpRequest.prototype.open = function(method, url, async, user, pass) {" +
                    "window.openHTTPs++;" +
                    "this.addEventListener('readystatechange', function() {" +
                    "if(this.readyState == 4) {" +
                    "window.openHTTPs--;" +
                    "}" +
                    "}, false);" +
                    "oldOpen.call(this, method, url, async, user, pass);" +
                    "}" +
                    "})();";
                jsDriver.ExecuteScript(script);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public IWebElement FindElement()
        {
            WaitUntilIsPresent();
            WaitForNetwork();
            return webdriver.FindElement(Path);
        }
    }
}