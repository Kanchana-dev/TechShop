using Microsoft.AspNetCore.Components;
using Shopping_App.Enums;

namespace Shopping_App.Pages
{
	public class OrdersBase: ComponentBase
	{
		//On a project,this would be another Web API call to a service called lets say ProductService 
		//to fetch Products related data. For POC purposes Enum is used to test code behind and Razor
		//page UI @code block.
		public static string GetProductName(int id)
		{
			return Enum.GetName(typeof(OrdersEnum), id)!;
		}
	}
}
