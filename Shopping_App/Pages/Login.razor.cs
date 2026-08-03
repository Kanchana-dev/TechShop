using Shopping_App.Models;

namespace Shopping_App.Pages
{
	public partial class Login
	{
		private string _currentSortColumn = "ProvidedDate";
		private bool _isAscending = true;
		private LoginModel model = new LoginModel();
		private string? error;
		protected IReadOnlyList<Weather>? Weather { get; set; } = null;
		protected IReadOnlyList<Weather>? SortedWeather { get; set; } = null;
		protected bool IsLoading { get; set; } = true;
		protected string? ErrorMessage { get; set; }

		protected override async Task OnInitializedAsync()
		{
			if (AuthService.token != null)
			{
				NavigationManager.NavigateTo("/home");
			}

			await GetWeatherDetails();
		}

		private async Task HandleValidSubmit()
		{
			try
			{
				await AuthService.Login(model);
				if (AuthService.token != null)
				{
					NavigationManager.NavigateTo("/home");
				}
				else
				{
					error = "Invalid Login";
				}
			}
			catch (Exception ex)
			{
				error = ex.Message;
				StateHasChanged();
			}
		}

		protected async Task GetWeatherDetails()
		{
			IsLoading = true;
			ErrorMessage = null;
			StateHasChanged();

			try
			{
				await Task.Delay(1000);
				Weather = await weatherService.GetWeatherDetails();
				if (Weather?.Count == 0) return;
				ApplyClientSideSort();
			}
			catch (Exception ex)
			{
				ErrorMessage = $"Failed to get weather details{Environment.NewLine}{ex.Message}";
			}
			finally
			{
				IsLoading = false;
			}
		}

		protected void Sort(string columnName)
		{
			if (_currentSortColumn == columnName)
			{
				_isAscending = !_isAscending;
			}
			else
			{
				_currentSortColumn = columnName;
				_isAscending = true;
			}

			ApplyClientSideSort();
		}

		private void ApplyClientSideSort()
		{
			Func<Weather, object> keySelector = _currentSortColumn switch
			{
				"ProvidedDate" => weather => weather?.ProvidedDate,
				"MaxTemp" => weather => weather?.MaxTemp ?? 0,
				"MinTemp" => weather => weather?.MinTemp ?? 0,
				"Precipitation" => weather => weather?.Precipitation ?? 0,
				"Error" or _ => weather => weather?.ErrorMessage ?? string.Empty
			};

			SortedWeather = _isAscending
				? Weather?.OrderBy(keySelector).ToList()
				: Weather?.OrderByDescending(keySelector).ToList();
		}

		protected string GetSortIcon(string columnName)
		{
			if (_currentSortColumn != columnName) return "⯁";
			return _isAscending ? "▲" : "▼";
		}
	}
}

