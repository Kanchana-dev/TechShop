TechShop is a Blazor WebAssembly project built in .NET 8.0
1.	Shopping_App is a Blazor Web App project that talks to Three RESTful Web APIs - AuthenticationAPI, OrdersAPI, ProductsAPI
2.	PLEASE use  Username - test, Password -any string for Successful Login.
3.	Logging and Authentication are provided with JSON Web Tokens.
4.	Adding users is under works. If more uers are needed, Please add in AuthenticationAPI/users.json
5.	Session state is nullified on Shopping_App Error. Please update URL to "http://localhost:1234/" that prompts to login page. Would have to re-login.
6.	Sample JWT for Swagger or Postman:    eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsImp0aSI6ImMwNTIzZTk3LWE3YTMtNGIyYy05YzBiLWE5MDY4NmY4NGIxOCIsIm5iZiI6MTcyNjg1MzM3MiwiZXhwIjoxNzI5NDQ1MzcyfQ.piRvGrWhr3dAShRn9SObpCzkKPP1mIwhWQKUsIgg0Mg
   Expires: 2024-10-20T17:29:32.000Z
8.	OrdersAPI and ProductsAPI are two RESTful Web API projects coded in .NET 8.0 Authorized to look for JWT Bearer.
9.	All projects have respective xUnit Test(Moq) projects.
10.	For POC purposes, Data is handled in JSON files mimicking no sql databases like SQLite.
    AuthenticationAPI project --> users.json
  	OrdersAPI project --> orders.json,
    ProductsAPI project --> products.json
  	Provided commented out code snippets for SQLite, Entity Framework DbContext/Models configuration and usage. Data folder in Shopping_App project has commented out SQLite components.
11.	Logging is performed with Serilog
12.	GlobalExceptionHandlers are added in Middleware
13.	CancellationTokens are implemented.
14.	Exception handling is provided in all projects
15.	DI pattern is used across all projects
16.	Positive and negative unit tests are coded and tested
17.	Solution Runs with BuildOrder – AuthenticationAPI, OrdersAPI, ProductsAPI, Shopping_App
18.	Solution should be configured for Multiple StartUp porojects – AuthenticationAPI, OrdersAPI, ProductsAPI,Shopping_App
19.	API projects are enabled with Swagger testing. 
20.	Error.Razor shows any unhandled errors in Shopping_App. ErrorBoundry is utilized in MainLayout.razor
21.	Orders.razor has both C# code written in razor page and also a code behind file using ComponentBase
 
*******AuthenticationAPI/users.json is the validation file for users. To add new users, add a new user in users.json with a new GUID as userid.
*******orders.json is the orders data file in OrdersAPI. This can be edited to delete records if needed.
