using System.Drawing;

namespace SeleniumCsharpE2e.Config.ConfigObjects
{
    public class ScreenshotObject
    {
        public string TestName { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string MethodPath { get; set; }
        public string AdditionalName { get; set; }
        public Bitmap Test { get; set; }
        public Bitmap Ref { get; set; }
        public Bitmap Result { get; set; }
    }
}
