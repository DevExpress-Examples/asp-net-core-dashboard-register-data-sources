using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using Microsoft.Extensions.FileProviders;

namespace WebDashboardDataSources.Configuration {
    public class ExtractDataSourceConfigurator {
        private static IFileProvider fileProvider;
        public static void ConfigureDataSource(DashboardConfigurator configurator, DataSourceInMemoryStorage storage, IFileProvider fileProvider) {
            // Registers an Extract data source.
            DashboardExtractDataSource extractDataSource = new DashboardExtractDataSource("Extract Data Source");
            extractDataSource.Name = "Extract Data Source";
            extractDataSource.ConnectionName = "dataExtract";
            storage.RegisterDataSource("extractDataSource ", extractDataSource.SaveToXml());

            ExtractDataSourceConfigurator.fileProvider = fileProvider;

            configurator.ConfigureDataConnection += Configurator_ConfigureDataConnection;
        }

        private static void Configurator_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if (e.ConnectionName =="dataExtract") {
                ExtractDataSourceConnectionParameters extractParams = new ExtractDataSourceConnectionParameters();
                extractParams.FileName = fileProvider.GetFileInfo("Data/SalesPersonExtract.dat").PhysicalPath;
                e.ConnectionParameters = extractParams;
            }
        }
    }
}
