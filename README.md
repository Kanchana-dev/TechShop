TechShop is a Blazor WebAssembly project built in .NET 8.0
1.	Shopping_App is a Blazor Web App project that talks to Three RESTful Web APIs - AuthenticationAPI, OrdersAPI, ProductsAPI
2.	PLEASE use  Username - test, Password -any string for Successful Login.
3.	Logging and Authentication are provided with JSON Web Tokens.
4.	Adding users is under works. If more uers are needed, Please add in AuthenticationAPI/users.json
5.	Session state is nullified on Shopping_App Error. Please update URL to "http://localhost:1234/" that prompts to login page. Would have to re-login.
6.	OrdersAPI and ProductsAPI are two RESTful Web API projects coded in .NET 8.0 Authorized to look for JWT Bearer.
7.	All projects have respective xUnit Test(Moq) projects.
8.	For POC purposes, Data is handled in JSON files mimicking no sql databases like SQLite.
    AuthenticationAPI project --> users.json
  	OrdersAPI project --> orders.json,
    ProductsAPI project --> products.json
  	Provided commented out code snippets for SQLite, Entity Framework DbContext/Models configuration and usage. Data folder in Shopping_App project has commented out SQLite components.
9.	Logging is performed with Serilog
10.	GlobalExceptionHandlers are added in Middleware
11.	CancellationTokens are implemented.
12.	Exception handling is provided in all projects
13.	DI pattern is used across all projects
14.	Positive and negative unit tests are coded and tested
15.	Solution Runs with BuildOrder – AuthenticationAPI, OrdersAPI, ProductsAPI, Shopping_App
16.	Solution should be configured for Multiple StartUp porojects – AuthenticationAPI, OrdersAPI, ProductsAPI,Shopping_App
17.	API projects are enabled with Swagger testing. 
18.	Error.Razor shows any unhandled errors in Shopping_App. ErrorBoundry is utilized in MainLayout.razor
19.	Orders.razor has both C# code written in razor page and also a code behind file using ComponentBase
 
*******AuthenticationAPI/users.json is the validation file for users. To add new users, add a new user in users.json with a new GUID as userid.
*******orders.json is the orders data file in OrdersAPI. This can be edited to delete records if needed.
