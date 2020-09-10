using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Blog.Core.Common
{
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }

        static Appsettings()
        {
            var path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .Add(new JsonConfigurationSource() {Path = path, Optional = false, ReloadOnChange = true})
                .Build();
        }

        public static string App(params string[] sections)
        {
            try
            {
                var val = sections.Aggregate(string.Empty, (current, t) => current + (t + ":"));

                return Configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {
                return "";
            }

        }
    }
}