using DevExpress.AspNetCore;
using DevExpress.DashboardAspNetCore;
using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using WebDashboardDataSources.Configuration;

namespace WebDashboardDataSources {
    public class Startup {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment) {
            Configuration = configuration;
            FileProvider = hostingEnvironment.ContentRootFileProvider;
            DashboardExportSettings.CompatibilityMode = DashboardExportCompatibilityMode.Restricted;            
        }

        public IFileProvider FileProvider { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddScoped<DashboardConfigurator>((IServiceProvider serviceProvider) => {
                DashboardConfigurator configurator = new DashboardConfigurator();

                // Create and configure a dashboard storage.
                DashboardFileStorage dashboardFileStorage = new DashboardFileStorage(FileProvider.GetFileInfo("Data/Dashboards").PhysicalPath);
                configurator.SetDashboardStorage(dashboardFileStorage);

                // Create and configure a data source storage.
                DataSourceInMemoryStorage dataSourceStorage = new DataSourceInMemoryStorage();
                
                SqlDataSourceConfigurator.ConfigureDataSource(dataSourceStorage); 
                ExcelDataSourceConfigurator.ConfigureDataSource(configurator, dataSourceStorage, FileProvider);
                ObjectDataSourceConfigurator.ConfigureDataSource(configurator, dataSourceStorage);
                EFDataSourceConfigurator.ConfigureDataSource(dataSourceStorage);
                JsonDataSourceConfigurator.ConfigureDataSource(configurator, dataSourceStorage, FileProvider);
                ExtractDataSourceConfigurator.ConfigureDataSource(configurator, dataSourceStorage, FileProvider);
                OlapDataSourceConfigurator.ConfigureDataSource(configurator, dataSourceStorage);
                XpoDataSourceConfigurator.ConfigureDataSource(dataSourceStorage);

                configurator.SetDataSourceStorage(dataSourceStorage);

                // Uncomment the next line to allow users to create new data sources based on predefined connection strings.
                // configurator.SetConnectionStringsProvider(new DashboardConnectionStringsProvider(Configuration));

                return configurator;
            });

            services.AddDevExpressControls();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseDevExpressControls();
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                // Map dashboard routes.
                endpoints.MapDashboardRoute("api/dashboard", "DefaultDashboard");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
