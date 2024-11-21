using MauiAppDemo.ViewModels;

namespace MauiAppDemo.Pages;

public partial class HomePage : ContentPage
{
    private HomeViewModel _viewModel;

    public HomePage()
	{
		InitializeComponent();
	}

    public HomePage(HomeViewModel viewModel)
    {

        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = viewModel;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.UpgradeHomeViewModel();

        // Simulación de una tarea asincrónica para cargar datos
        // await Task.Delay(1000); // Simulación de un retraso

        // Aquí puedes actualizar el ViewModel con datos obtenidos asincrónicamente
        //_viewModel.Message = "Datos cargados exitosamente!";
    }

}