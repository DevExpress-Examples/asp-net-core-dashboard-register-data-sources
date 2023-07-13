using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace WebDashboardDataSources.Configuration {
    public class ExcelDataSourceConfigurator {
        private static IFileProvider fileProvider;
        public static void ConfigureDataSource(DashboardConfigurator configurator, DataSourceInMemoryStorage storage, IFileProvider fileProvider) {
            // Registers an Excel data source.
            DashboardExcelDataSource excelDataSource = new DashboardExcelDataSource("Excel Data Source");
            excelDataSource.ConnectionName = "excelDataConnection";
            excelDataSource.SourceOptions = new ExcelSourceOptions(new ExcelWorksheetSettings("Sheet1"));
            storage.RegisterDataSource("excelDataSource", excelDataSource.SaveToXml());

            ExcelDataSourceConfigurator.fileProvider = fileProvider;

            configurator.ConfigureDataConnection += Configurator_ConfigureDataConnection;
        }


        private static void Configurator_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if (e.ConnectionName == "excelDataConnection") {
                ExcelDataSourceConnectionParameters excelParams = new ExcelDataSourceConnectionParameters(fileProvider.GetFileInfo("Data/Sales.xlsx").PhysicalPath);
                e.ConnectionParameters = excelParams;
            }
        }
    }
}
