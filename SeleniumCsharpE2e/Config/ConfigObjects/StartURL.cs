using System.Configuration;

namespace SeleniumCsharpE2e.Config.ConfigObjects
{
    /// <summary>
    /// App.Config element model for the TestEnvironment to use
    /// </summary>
    internal class StartURL : ConfigurationElement
    {
        /// <summary>
        /// 
        /// </summary>
		[ConfigurationProperty("url", IsRequired = true, IsKey = true)]
        internal string URL
        {
            get { return (string)this["url"]; }
            set { this["url"] = value; }
        }
    }
}
