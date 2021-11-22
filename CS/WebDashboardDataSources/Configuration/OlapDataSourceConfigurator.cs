using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;

namespace WebDashboardDataSources.Configuration {
    public class OlapDataSourceConfigurator {
        public static void ConfigureDataSource(DashboardConfigurator configurator, DataSourceInMemoryStorage storage) {
            // Registers an OLAP data source.
            DashboardOlapDataSource olapDataSource = new DashboardOlapDataSource("OLAP Data Source", "olapConnection");            
            storage.RegisterDataSource("olapDataSource", olapDataSource.SaveToXml());

            configurator.ConfigureDataConnection += Configurator_ConfigureDataConnection;
        }


        private static void Configurator_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if (e.ConnectionName == "olapConnection") {
                OlapConnectionParameters olapParams = new OlapConnectionParameters();
                olapParams.ConnectionString = "Provider=MSOLAP;Data Source=http://demos.devexpress.com/Services/OLAP/msmdpump.dll;"
                    + "Initial catalog=Adventure Works DW Standard Edition;Cube name=Adventure Works;Query Timeout=100;";
                e.ConnectionParameters = olapParams;
            }
        }
    }
}