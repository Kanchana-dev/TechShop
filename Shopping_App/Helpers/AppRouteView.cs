using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Shopping_App.Services;

namespace Shopping_App.Helpers
{
	public class AppRouteView : RouteView
	{
		[Inject]
		public NavigationManager NavigationManager { get; set; }

		[Inject]
		public IAuthService AuthService { get; set; }

		protected override void Render(RenderTreeBuilder builder)
		{
			var authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) != null;		
			{
				base.Render(builder);
			}
		}
	}
}
