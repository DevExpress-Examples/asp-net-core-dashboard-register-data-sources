*Files to look at*:

* [Startup.cs](./CS/WebDashboardDataSources/Startup.cs)

## How to Register Data Sources for ASP.NET Core Dashboard

The following example displays how to provide a Web Dashboard with a set of predefined data sources available for end users.

![](web-dashboard-data-sources.png)

Supported data sources:

- [SQL data source](http://docs.devexpress.com/Dashboard/116652/)
- [OLAP data source (XMLA only)](http://docs.devexpress.com/Dashboard/400562/)
- [Excel data source](http://docs.devexpress.com/Dashboard/116654/)
- [Object data source](http://docs.devexpress.com/Dashboard/116655/)
- [Entity Framework data source](http://docs.devexpress.com/Dashboard/116656/)
- [Extract data source](http://docs.devexpress.com/Dashboard/116657/)
- [JSON data source](http://docs.devexpress.com/Dashboard/401224/)
- [XPO data source](http://docs.devexpress.com/Dashboard/401226/)

The [DashboardConfigurator.SetDataSourceStorage](http://docs.devexpress.com/Dashboard/DevExpress.DashboardWeb.DashboardConfigurator.SetDataSourceStorage.overloads) method is used to register the added data sources in a data source storage. 
