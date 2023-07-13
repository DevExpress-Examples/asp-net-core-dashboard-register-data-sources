using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.EntityFramework;

namespace WebDashboardDataSources.Configuration {
    public class EFDataSourceConfigurator {
        public static void ConfigureDataSource(DataSourceInMemoryStorage storage) {
            // Registers an Entity Framework data source.
            DashboardEFDataSource efDataSource = new DashboardEFDataSource("EF Data Source");            
            efDataSource.ConnectionParameters = new EFConnectionParameters(typeof(OrdersContext));
            storage.RegisterDataSource("efDataSource", efDataSource.SaveToXml());
        }
    }
}
