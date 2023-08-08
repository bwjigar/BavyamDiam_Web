using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;

namespace SunriseWeb.Resources
{
    public class LanguageProvider
    {
        private static ResourceManager resourceManager;
        private static ResourceManager defaultResourceManager = language.ResourceManager;

        public static void setCultureLanguage(string langCode)
        {
            switch (langCode)
            {
                case "cn":
                    resourceManager = language_cn.ResourceManager;
                    break;
                default:
                    resourceManager = language.ResourceManager;
                    break;
            }
        }
        public static string get(string key)
        {
            try
            {
                string value = resourceManager.GetString(key);
                if (string.IsNullOrEmpty(value))
                {
                    value = defaultResourceManager.GetString(key);
                }

                return value;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}