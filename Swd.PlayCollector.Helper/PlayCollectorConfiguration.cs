using Microsoft.Extensions.Configuration;
using System.IO;

namespace Swd.PlayCollector.Helper
{
    public class PlayCollectorConfiguration
    {
        public string PathToMediafiles { get; set; }
        public string PathToRessourceFiles { get; set; }
        public string PathToPlaceholderImage { get; set; }

        public PlayCollectorConfiguration() 
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddEnviromentVariables()
                .Build();

            PathToMediafiles = configuration.GetValue<string>("PathSettings:PATH_RootMediafiles").ToString();

            PathToRessourceFiles = 
                string.Format("{0}\\{1}", Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                configuration.GetValue<string>("PathSettings:PATH_RootResourceImages").ToString());

            PathToPlaceholderImage =
                string.Format("{0}\\{1}", PathToRessourceFiles,
                configuration.GetValue<string>("PathSettings:FILE_PlaceholderImage").ToString()

                );
        }
    }
}
