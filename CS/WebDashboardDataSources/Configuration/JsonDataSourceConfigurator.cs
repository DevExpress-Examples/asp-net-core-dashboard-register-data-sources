using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Json;
using Microsoft.Extensions.FileProviders;
using System;

namespace WebDashboardDataSources.Configuration {
    public class JsonDataSourceConfigurator {
        private static IFileProvider fileProvider;
        public static void ConfigureDataSource(DashboardConfigurator configurator, DataSourceInMemoryStorage storage, IFileProvider fileProvider) {
            // Registers a JSON data source from URL.
            DashboardJsonDataSource jsonDataSourceUrl = new DashboardJsonDataSource("JSON Data Source (URL)");
            jsonDataSourceUrl.ConnectionName = "jsonUrlConnection";
            jsonDataSourceUrl.RootElement = "Customers";
            storage.RegisterDataSource("jsonDataSourceUrl", jsonDataSourceUrl.SaveToXml());

            // Registers a JSON data source from a JSON file.
            DashboardJsonDataSource jsonDataSourceFile = new DashboardJsonDataSource("JSON Data Source (File)");
            jsonDataSourceFile.ConnectionName = "jsonFileConnection";
            jsonDataSourceFile.RootElement = "Customers";
            storage.RegisterDataSource("jsonDataSourceFile", jsonDataSourceFile.SaveToXml());

            // Registers a JSON data source from JSON string.
            DashboardJsonDataSource jsonDataSourceString = new DashboardJsonDataSource("JSON Data Source (String)");
            jsonDataSourceString.ConnectionName = "jsonStringConnection";
            jsonDataSourceString.RootElement = "Customers";
            storage.RegisterDataSource("jsonDataSourceString", jsonDataSourceString.SaveToXml());

            JsonDataSourceConfigurator.fileProvider = fileProvider;

            configurator.ConfigureDataConnection += Configurator_ConfigureDataConnection;
        }

        private static void Configurator_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if (e.ConnectionName == "jsonFileConnection") {
                Uri fileUri = new Uri(fileProvider.GetFileInfo("Data/customers.json").PhysicalPath, UriKind.RelativeOrAbsolute);
                JsonSourceConnectionParameters jsonParams = new JsonSourceConnectionParameters();
                jsonParams.JsonSource = new UriJsonSource(fileUri);
                e.ConnectionParameters = jsonParams;
            }
            if (e.ConnectionName == "jsonUrlConnection") {
                JsonSourceConnectionParameters jsonParams = new JsonSourceConnectionParameters();
                jsonParams.JsonSource = new UriJsonSource(new Uri("https://raw.githubusercontent.com/DevExpress-Examples/DataSources/master/JSON/support.json"));
                e.ConnectionParameters = jsonParams;
            }
            if (e.ConnectionName == "jsonStringConnection") {
                string json = "{\"Customers\":[{\"Id\":\"ALFKI\",\"CompanyName\":\"Alfreds Futterkiste\",\"ContactName\":\"Maria Anders\",\"ContactTitle\":\"Sales Representative\",\"Address\":\"Obere Str. 57\",\"City\":\"Berlin\",\"PostalCode\":\"12209\",\"Country\":\"Germany\",\"Phone\":\"030-0074321\",\"Fax\":\"030-0076545\"}],\"ResponseStatus\":{}}";
                JsonSourceConnectionParameters jsonParams = new JsonSourceConnectionParameters();
                jsonParams.JsonSource = new CustomJsonSource(json);
                e.ConnectionParameters = jsonParams;
            }
        }
    }
}