using CommunityToolkit.Mvvm.ComponentModel;
using MauiAppDemo.Core.Services;

namespace MauiAppDemo.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly AuthService _authService;

        // ObservableProperty for message
        [ObservableProperty]
        private string message;

        public HomeViewModel()
        {
           
        }

        // Constructor - AuthService is injected via DI
        public HomeViewModel(AuthService authService)
        {
            _authService = authService;
            Message = "Please log in to the app."; // Initial message
        }

        // Async method to upgrade the message based on authentication status
        public async Task UpgradeHomeViewModel()
        {
            try
            {
                // Check if the user is authenticated
                if (_authService != null)
                {
                    var isAuthenticated = await _authService.CheckIsAuthenticated();

                    if (isAuthenticated)
                    {
                        // Fetch user info if authenticated
                        var info = await _authService.GetInfoUser();

                        // Use string interpolation correctly
                        Message = $"Hello, {info.Name}, Welcome to your new app.";
                    }
                    else
                    {
                        Message = "Authentication failed. Please log in.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur (e.g., network issues, service failures)
                Message = $"An error occurred: {ex.Message}";
            }
        }
    }
}
