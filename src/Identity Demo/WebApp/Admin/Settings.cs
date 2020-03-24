using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace WebApp.Admin
{
    /// <summary>
    /// A collection of application wide settings that provide values which
    /// have been set in the web.config's <appSettings /> section.
    /// </summary>
    internal class Settings
    {
        public static string AdminRole => ConfigurationManager.AppSettings["adminRole"];
        public static string AdminUser => ConfigurationManager.AppSettings["adminUserName"];
    }
}