using System;
using OpenQA.Selenium;

namespace SeleniumE2ECsharp.Config
{
    public static class DriverContext
    {
        public static IWebDriver Current { get; private set; }

        public static void SetDriver(IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException(nameof(driver), "Driver não pode ser nulo");
            }
            Current = driver;
        }

        public static void QuitDriver()
        {
            if (Current != null)
            {
                Current.Quit();
                Current = null;
            }
        }
    }
}