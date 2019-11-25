using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.EntityFramework;
using DevExpress.DataAccess.Excel;
using DevExpress.DataAccess.Json;
using DevExpress.DataAccess.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace WebDashboardDataSources {
    public class Startup {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) {
            Configuration = configuration;
            FileProvider = hostingEnvironment.ContentRootFileProvider;
            DashboardExportSettings.CompatibilityMode = DashboardExportCompatibilityMode.Restricted;
            DashboardOlapDataSource.OlapDataProvider = OlapDataProviderType.Xmla;
        }

        public IFileProvider FileProvider { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services
                .AddMvc()
                .AddDefaultDashboardController((configurator, serviceProvider)  => {
                    configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(Configuration));

                    DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(FileProvider.GetFileInfo("Data/Dashboards").PhysicalPath);
                    configurator.SetDashboardStorage(dashboardFileStorage);
                    configurator.SetDataSourceStorage(CreateDataSourceStorage());

                    configurator.DataLoading += (s, e) => {
                        if(e.DataSourceName == "Object Data Source") {
                            e.Data = Invoices.CreateData();
                        }
                    };

                    configurator.ConfigureDataConnection += Configurator_ConfigureDataConnection;
                });

            services.AddDevExpressControls(options => options.Resources = ResourcesType.ThirdParty | ResourcesType.DevExtreme);
        }

        public DataSourceInMemoryStorage CreateDataSourceStorage() {
            DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();

            // Registers an SQL data source.
            DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("SQL Data Source", "NWindConnectionString");
            sqlDataSource.DataProcessingMode = DataProcessingMode.Client;
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("Categories")
                .Join("Products", "CategoryID")
                .SelectAllColumns()
                .Build("Products_Categories");
            sqlDataSource.Queries.Add(query);
            dataSourceStorage.RegisterDataSource("sqlDataSource", sqlDataSource.SaveToXml());

            // Registers an OLAP data source.
            DashboardOlapDataSource olapDataSource = new DashboardOlapDataSource("OLAP Data Source", "olapConnection");
            dataSourceStorage.RegisterDataSource("olapDataSource", olapDataSource.SaveToXml());

            // Registers an Excel data source.
            DashboardExcelDataSource excelDataSource = new DashboardExcelDataSource("Excel Data Source");
            excelDataSource.FileName = FileProvider.GetFileInfo("Data/Sales.xlsx").PhysicalPath;
            excelDataSource.SourceOptions = new ExcelSourceOptions(new ExcelWorksheetSettings("Sheet1"));
            dataSourceStorage.RegisterDataSource("excelDataSource", excelDataSource.SaveToXml());

            // Registers an Object data source.
            DashboardObjectDataSource objDataSource = new DashboardObjectDataSource("Object Data Source");
            dataSourceStorage.RegisterDataSource("objDataSource", objDataSource.SaveToXml());

            // Registers a EF Core data source.
            DashboardEFDataSource efDataSource = new DashboardEFDataSource("EF Core Data Source");
            efDataSource.ConnectionParameters = new EFConnectionParameters(typeof(OrdersContext));
            dataSourceStorage.RegisterDataSource("efDataSource", efDataSource.SaveToXml());

            // Registers an Extract data source.
            DashboardExtractDataSource extractDataSource = new DashboardExtractDataSource("Extract Data Source");
            extractDataSource.Name = "Extract Data Source";
            extractDataSource.FileName = FileProvider.GetFileInfo("Data/SalesPersonExtract.dat").PhysicalPath;
            dataSourceStorage.RegisterDataSource("extractDataSource ", extractDataSource.SaveToXml());
            
            // Registers a JSON data source from URL.
            DashboardJsonDataSource jsonDataSourceUrl = new DashboardJsonDataSource("JSON Data Source (URL)");
            jsonDataSourceUrl.JsonSource = new UriJsonSource(new Uri("https://raw.githubusercontent.com/DevExpress-Examples/DataSources/master/JSON/customers.json"));
            jsonDataSourceUrl.RootElement = "Customers";
            jsonDataSourceUrl.Fill();
            dataSourceStorage.RegisterDataSource("jsonDataSourceUrl", jsonDataSourceUrl.SaveToXml());

            // Registers a JSON data source from a JSON file.
            DashboardJsonDataSource jsonDataSourceFile = new DashboardJsonDataSource("JSON Data Source (File)");
            Uri fileUri = new Uri(FileProvider.GetFileInfo("Data/customers.json").PhysicalPath, UriKind.RelativeOrAbsolute);
            jsonDataSourceFile.JsonSource = new UriJsonSource(fileUri);
            jsonDataSourceFile.RootElement = "Customers";
            jsonDataSourceFile.Fill();
            dataSourceStorage.RegisterDataSource("jsonDataSourceFile", jsonDataSourceFile.SaveToXml());

            // Registers a JSON data source from JSON string.
            DashboardJsonDataSource jsonDataSourceString = new DashboardJsonDataSource("JSON Data Source (String)");
            string json = "{\"Customers\":[{\"Id\":\"ALFKI\",\"CompanyName\":\"Alfreds Futterkiste\",\"ContactName\":\"Maria Anders\",\"ContactTitle\":\"Sales Representative\",\"Address\":\"Obere Str. 57\",\"City\":\"Berlin\",\"PostalCode\":\"12209\",\"Country\":\"Germany\",\"Phone\":\"030-0074321\",\"Fax\":\"030-0076545\"}],\"ResponseStatus\":{}}";
            jsonDataSourceString.JsonSource = new CustomJsonSource(json);
            jsonDataSourceString.RootElement = "Customers";
            jsonDataSourceString.Fill();
            dataSourceStorage.RegisterDataSource("jsonDataSourceString", jsonDataSourceString.SaveToXml());

            // Registers an XPO data source.
            DashboardXpoDataSource xpoDataSource = new DashboardXpoDataSource("XPO Data Source");
            xpoDataSource.ConnectionStringName = "NWindConnectionString";
            xpoDataSource.SetEntityType(typeof(Category));
            dataSourceStorage.RegisterDataSource("xpoDataSource", xpoDataSource.SaveToXml());

            return dataSourceStorage;
        }

        private void Configurator_ConfigureDataConnection(object sender, ConfigureDataConnectionWebEventArgs e) {
            if(e.ConnectionName == "olapConnection") {
                OlapConnectionParameters olapParams = new OlapConnectionParameters();
                olapParams.ConnectionString = "Provider=MSOLAP;Data Source=http://demos.devexpress.com/Services/OLAP/msmdpump.dll;"
                    + "Initial catalog=Adventure Works DW Standard Edition;Cube name=Adventure Works;Query Timeout=100;";
                e.ConnectionParameters = olapParams;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseDevExpressControls();
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapDashboardRoute();
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
