<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/206556408/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T828517)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# BI Dashboard for ASP.NET Core - How to Register Data Sources


The following example displays how to supply a Web Dashboard with a set of predefined data sources available for users.

![](web-dashboard-data-sources.png)

The [DashboardConfigurator.SetDataSourceStorage](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.SetDataSourceStorage.overloads) method is used to register the added data sources in data source storage. 

The [DashboardConfigurator.ConfigureDataConnection](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.ConfigureDataConnection) event is handled to customize connection parameters before the Web Dashboard connects to a data store (database, OLAP cube, etc.).

## Files to Review

* [EFDataSourceConfigurator.cs](./CS/WebDashboardDataSources/Configuration/EFDataSourceConfigurator.cs)
* [ExcelDataSourceConfigurator.cs](./CS/WebDashboardDataSources/Configuration/ExcelDataSourceConfigurator.cs)
* [ExtractDataSourceConfigurator.cs](./CS/WebDashboardDataSources/Configuration/ExtractDataSourceConfigurator.cs)
* [JsonDataSourceConfigurator.cs](./CS/WebDashboardDataSources/Configuration/JsonDataSourceConfigurator.cs)
* [ObjectDataSourceConfigurator.cs](./CS/WebDashboardDataSources/Configuration/ObjectDataSourceConfigurator.cs)
* [OlapDataSourceConfigurator.cs](./CS/WebDashboardDataSources/Configuration/OlapDataSourceConfigurator.cs)
* [SqlDataSourceConfigurator.cs](./CS/WebDashboardDataSources/Configuration/SqlDataSourceConfigurator.cs)
* [XpoDataSourceConfigurator.cs](./CS/WebDashboardDataSources/Configuration/XpoDataSourceConfigurator.cs)
* [Startup.cs](./CS/WebDashboardDataSources/Startup.cs)

## Documentation

- [Register Default Data Sources](https://docs.devexpress.com/Dashboard/116482/web-dashboard/dashboard-backend/register-default-data-sources)

## See Also

- [How to Register Data Sources for ASP.NET Web Forms Dashboard Control](https://github.com/DevExpress-Examples/asp-net-web-forms-dashboard-register-data-sources)
- [How to Register Data Sources for ASP.NET MVC Dashboard Extension](https://github.com/DevExpress-Examples/asp-net-mvc-dashboard-register-data-sources)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=asp-net-core-dashboard-register-data-sources&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=asp-net-core-dashboard-register-data-sources&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
