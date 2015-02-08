using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Ffd.Presentation.Website
{
    public class WebConfig
    {
        /// <summary>
        /// Content server for the website
        /// </summary>
        public static string ContentServer
        {
            get
            {
                return GetAppSetting("ContentServer");
            }
        }

        public static string ImageGeneratorUrl
        {
            get
            {
                return GetAppSetting("ImageGeneratorUrl");
            }
        }

        /// <summary>
        /// Quandry: allow direct access to this function (quick), or require a property for each (intellsense)?
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        private static string GetAppSetting(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }
    }
}
