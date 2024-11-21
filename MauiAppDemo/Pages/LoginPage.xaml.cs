using MauiAppDemo.ViewModels;

namespace MauiAppDemo.Pages
{
    public partial class LoginPage : ContentPage
    {
        
        public LoginPage()
        {
            InitializeComponent();

        }
        
        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

        }

    }
}