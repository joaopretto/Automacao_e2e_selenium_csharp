using SeleniumCsharpE2e.Config.ConfigObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SeleniumCsharpE2e.PageObject;

namespace SeleniumCsharpE2e.Config
{
    public class PrintManager : BasePage
    {
        string screenshotDir => Directory.GetCurrentDirectory();

        public void Print(string name, bool failure = false)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }

            name = Normalize(name);

            if (name.Length > 200)
            {
                name = name.Substring(0, 200);
            }

            if (failure)
            {
                name += "_" + TestCaseObject.CurrentTestCase.RetryCount;
            }

            Screenshot sct = ((ITakesScreenshot)webdriver).GetScreenshot();

            string methodPath = TestContext.CurrentContext.Test.ClassName.Split('.').Last() + "." + TestContext.CurrentContext.Test.MethodName;
            string path = screenshotDir + @"\" + name + ".png";

            byte[] imageBytes = Convert.FromBase64String(sct.ToString());

            using (BinaryWriter bw = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
            {
                bw.Write(imageBytes);
                bw.Close();
                Console.WriteLine("Screenshot taken: " + path);
            }

            if (!failure)
            {
                ScreenshotObject obj = new ScreenshotObject
                {
                    TestName = TestContext.CurrentContext.Test.Name,
                    FilePath = path,
                    MethodPath = methodPath,
                    Name = name,
                };
            }
        }

        public string Normalize(string text)
        {

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
