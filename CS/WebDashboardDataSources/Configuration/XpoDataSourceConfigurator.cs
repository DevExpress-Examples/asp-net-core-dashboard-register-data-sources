using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;

namespace WebDashboardDataSources.Configuration {
    public class XpoDataSourceConfigurator {
        public static void ConfigureDataSource(DataSourceInMemoryStorage storage) {
            // Registers an XPO data source.
            DashboardXpoDataSource xpoDataSource = new DashboardXpoDataSource("XPO Data Source");
            xpoDataSource.ConnectionStringName = "NWindConnectionString";
            xpoDataSource.SetEntityType(typeof(Category));
            storage.RegisterDataSource("xpoDataSource", xpoDataSource.SaveToXml());
        }
    }
}