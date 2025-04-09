using OpenQA.Selenium;
using SeleniumCsharpE2e.Config;

namespace SeleniumCsharpE2e.Config
{
    public abstract class HasWebDriverAccess
    {
        private static IWebDriver _webDriver;

        public IWebDriver WebDriver
        {
            get
            {
                if (_webDriver == null)
                {
                    _webDriver = DriverFactory.CreateDriver();
                }
                return _webDriver;
            }
        }

        public void DisposeWebDriver()
        {
            if (_webDriver != null)
            {
                _webDriver.Quit();
                _webDriver.Dispose();
                _webDriver = null;
            }
        }
    }
}