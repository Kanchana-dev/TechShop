TechShop is a Blazor WebAssembly project built in .NET 8.0
1.	Shopping_App is a Blazor Web App project that talks to two RESTful Web APIs.
2.	Logging and Authentication are not yet available. 2 factor Authentication with  OAuth and JSON Web Tokens(retrieve and refresh for session) would be ideal.
3.	OrdersAPI and ProductsAPI are 2 RESTful Web API projects coded in .NET 8.0 
4.	All 3 projects have respective xUnit Test(Moq) projects.
5.	For POC purposes, Data is handled in JSON files mimicking no sql databases like SQLite.
    OrdersAPI project --> orders.json
    ProductsAPI project --> products.json
  	Provided commented out code snippets for SQLite, Entity Framework DbContext/Models configuration and usage. Data folder in Shopping_App project has commented out SQLite components.
7.	Logging is performed with Serilog
8.	GlobalExceptionHandlers are added in Middleware
9.	Exception handling is provided in all projects
10.	DI pattern is used across all projects
11.	Positive and negative unit tests are coded and tested
12.	Solution Runs with BuildOrder – OrdersAPI, ProductsAPI, Shopping_App
13.	Solution should be configured for Multiple StartUp porojects – OrdersAPI, ProductsAPI,Shopping_App
14.	API projects are enabled with Swagger testing. 
15.	Error.Razor shows any unhandled errors in Shopping_App. ErrorBoundry is utilized in MainLayout.razor
16.	Orders.razor has both C# code written in razor page and also a code behind file using ComponentBase
orders.json is the data file in OrdersAPI. This can be edited to delete records if needed. 
