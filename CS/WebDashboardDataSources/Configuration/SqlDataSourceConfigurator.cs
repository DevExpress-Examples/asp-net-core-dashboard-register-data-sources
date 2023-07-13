using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Sql;
using System;

namespace WebDashboardDataSources.Configuration {
    public class SqlDataSourceConfigurator {
        public static void ConfigureDataSource(DataSourceInMemoryStorage storage) {
            // Registers an SQL data source.
            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "NWindConnectionString");
            sqlDataSource.DataProcessingMode = DataProcessingMode.Client;
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("Categories")
                .Join("Products", "CategoryID")
                .SelectAllColumnsFromTable()
                .Build("Products_Categories");
            sqlDataSource.Queries.Add(query);
            storage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());
        }
    }
}
