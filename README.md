TechShop is a Blazor WebAssembly project built in .NET 8.0

A new Web API - WeatherAPI is added to TechShop solution to display historical weather data on Login page. For login and rest of the TechShop project refer to notes in later part - **TechShop**.

**WeatherAPI**

Goal of this project is to read dates from a text file, pass those to open Meteo API to fetch weather report and save fetched data.

Architecture of the project:

Blazor UI loads --> Async call to Proxy API endpoint --> Behind Firewall API endpoint --> Read dates.txt, if stored previously, read it, if not call open Meteo API to get data --> send this payload back to UI --> display in a table.

Design Considerations:
1. Read only data.
2. Initially small input read but may grow eventually.
3. Error Handling.
4. Memory allocation - onprem or on cloud to spin off multi threads to read dates.txt and 'weather-data' json files.

Solution:
1. Created a new Web API project - WeatherAPI.
2. Structured the project with Controllers, BusinessRules, Repository, Service layers. Utilized DI with needed lifetimes.
3. Async/Await is used to let reading data from dates.txt,
   reading from directory 'weather-data' for existing files and
   writing new data into 'weather-data'
   happen in background multi threads while freeing the main app thread
4. Errors are handled via try/catch blocks, empty or bad inputs are sent back to UI in user friendly messages.
5. Once data is fetched for a date, this data is stored in its own json file in 'weather-data' folder. On GET call for a new date,
   this storage is accessed to see for data with filename, if yes, no external API call to Meteo is made. If 'weather-data' does
   not have a date json, external API call is made to Meteo, result is saved in 'weather-data'. This avoids unnecessary multiple calls
   to Meteo API.
6. Blazor UI displays results in a Table. Client-side Sorting is enabled.  
7. Razor pages provide user friendly warnings, errors.
8. Postman is utilized to run open meteo GET api calls, to inspect result set data. 


**TechShop**
1.	Shopping_App is a Blazor Web App project that talks to Three RESTful Web APIs - AuthenticationAPI, OrdersAPI, ProductsAPI
2.	Username - test, Password -any string for Successful Login.
3.	Logging and Authentication are provided with JSON Web Tokens.
4.	Adding users is under works. If more users are needed, add in AuthenticationAPI/users.json
5.	Session state is nullified on Shopping_App Error. Update URL to "http://localhost:1234/" that prompts to login page. Would have to re-login.
6.	Sample JWT for Swagger or Postman(Bearer Token):    eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdCIsImp0aSI6ImMwNTIzZTk3LWE3YTMtNGIyYy05YzBiLWE5MDY4NmY4NGIxOCIsIm5iZiI6MTcyNjg1MzM3MiwiZXhwIjoxNzI5NDQ1MzcyfQ.piRvGrWhr3dAShRn9SObpCzkKPP1mIwhWQKUsIgg0Mg                                                                  
   Expires: 2024-10-20T17:29:32.000Z
8.	OrdersAPI and ProductsAPI are two RESTful Web API projects coded in .NET 8.0 Authorized to look for JWT Bearer.
9.	Test Order to add in Postman
    {
     "orderId": "{{$guid}}",
     "productId": Take a Prod Id from orders.json,
     "productName": Take the corresponding ProductName from orders.json,
     "orderTime": "2024-09-23T20:07:09.962Z",
     "quantity": can be 1, 2 or 3,
     "userId": Take 'test' userid from users.json
  	 }
11.	All projects have respective xUnit Test(Moq) projects.
12.	For POC purposes, Data is handled in JSON files mimicking no sql databases like SQLite.
      AuthenticationAPI project --> users.json
  	   OrdersAPI project --> orders.json,
      ProductsAPI project --> products.json
  	   Provided commented out code snippets for SQLite, Entity Framework DbContext/Models configuration and usage. Data folder in Shopping_App project has
   	commented out SQLite components.
14.	Logging is performed with Serilog
15.	GlobalExceptionHandlers are added in Middleware
16.	CancellationTokens are implemented.
17.	Exception handling is provided in all projects
18.	DI pattern is used across all projects
19.	Positive and negative unit tests are coded and tested
20.	Solution Runs with BuildOrder – AuthenticationAPI, OrdersAPI, ProductsAPI, Shopping_App
21.	Solution should be configured for Multiple StartUp porojects – AuthenticationAPI, OrdersAPI, ProductsAPI,Shopping_App
22.	API projects are enabled with Swagger testing. 
23.	Error.Razor shows any unhandled errors in Shopping_App. ErrorBoundry is utilized in MainLayout.razor
24.	Orders.razor has both C# code written in razor page and also a code behind file using ComponentBase
 
*******AuthenticationAPI/users.json is the validation file for users. To add new users, add a new user in users.json with a new GUID as userid.
*******orders.json is the orders data file in OrdersAPI. This can be edited to delete records if needed.
