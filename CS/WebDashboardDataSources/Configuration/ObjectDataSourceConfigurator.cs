using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;

namespace WebDashboardDataSources.Configuration {
    public class ObjectDataSourceConfigurator {
        public static void ConfigureDataSource(DashboardConfigurator configurator, DataSourceInMemoryStorage storage) {
            // Registers an Object data source.
            DashboardObjectDataSource objDataSource = new DashboardObjectDataSource("Object Data Source");
            objDataSource.DataId = "objectDataSource";
            storage.RegisterDataSource("objDataSource", objDataSource.SaveToXml());

            configurator.DataLoading += DataLoading;

        }
        private static void DataLoading(object sender, DataLoadingWebEventArgs e) {
            if (e.DataId == "objectDataSource") {
                e.Data = Invoices.CreateData();
            }
        }
    }
}