using MauiAppDemo.ViewModels;

namespace MauiAppDemo.Pages 
{
    public partial class EmployeerList : ContentPage
    {
        private EmployerListViewModel _viewModel;

        public EmployeerList()
        {
            InitializeComponent();
        }

        public EmployeerList(EmployerListViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            BindingContext = viewModel;

            // Cargar los primeros empleados cuando la página aparezca
            LoadEmployees();

        }

        // Cargar los empleados iniciales
        private async void LoadEmployees()
        {
            await _viewModel.LoadEmployeesAsync();
        }

        // Evento que se ejecuta cuando un ítem aparece en la pantalla
        private async void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            // Verificar si el último ítem visible es el último ítem de la lista
            var lastItem = _viewModel.EmployeList[_viewModel.EmployeList.Count - 1];
            if (e.Item == lastItem)
            {
                // Cargar más empleados
                await _viewModel.LoadEmployeesAsync();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           
        }
    }


}

