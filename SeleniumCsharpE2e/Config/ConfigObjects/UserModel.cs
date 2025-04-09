using System;
using System.Collections.Generic;
using System.Configuration;

namespace SeleniumCsharpE2e.Config.ConfigObjects
{
    /// <summary>
    /// Simple model public class for modeling properties about user.  Intended
    /// to be extended from where testers see fit.  For now, we just
    /// mimic the user properties off of the Account Information screen.
    /// </summary>
    public class UserModel : ConfigurationElement
    {
        /// <summary>
        /// General dumping grounds for additional properties
        /// if extension public class seems like overkill.
        /// </summary>
        private IDictionary<string, object> userProperties;

        /// <summary>
        /// Default constructor
        /// </summary>
        internal UserModel()
        {
            userProperties = new Dictionary<string, object>();
        }

        /// <summary>
        /// Base URL affiliated with the user
        /// </summary>
        [ConfigurationProperty("environment", IsRequired = true, IsKey = true)]
        internal string Environment
        {
            get { return base["environment"] as string; }
            set { base["environment"] = value; }
        }

        /// <summary>
        /// Login username
        /// </summary>
        [ConfigurationProperty("username", IsRequired = true, IsKey = true)]
        internal string Username
        {
            get { return base["username"] as string; }
            set { base["username"] = value; }
        }

        /// <summary>
        /// Retrieve a user property
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal Object GetProperty(string key)
        {
            return userProperties[key];
        }

        /// <summary>
        /// Assign a user property
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal void SetProperty(string key, Object value)
        {
            userProperties[key] = value;
        }
    }
}
