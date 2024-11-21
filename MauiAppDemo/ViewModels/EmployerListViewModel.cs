using CommunityToolkit.Mvvm.ComponentModel;
using Front.Models.Entitities;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Collections.ObjectModel;

namespace MauiAppDemo.ViewModels
{
    public partial class EmployerListViewModel : ObservableObject
    {
    
        private ObservableCollection<EmployeeEntitie> _employeList { get; set; }

        public ObservableCollection<EmployeeEntitie> EmployeList 
        { 
            get => _employeList;
            set => _employeList = value;
        }

        private bool _isLoading;
        private int _pageNumber;
        private const int PageSize = 10;
        private const int Threshold = 5; // Se activa cuando quedan 5 elementos o menos



        public EmployerListViewModel()
        {
            EmployeList = new ObservableCollection<EmployeeEntitie>();
            _pageNumber = 1;
            _ = LoadEmployeesAsync();

            
        }

        // Comando que se ejecuta cuando el umbral de elementos restantes es alcanzado
        public IAsyncCommand RemainingItemsThresholdReachedCommand => new AsyncCommand(OnRemainingItemsThresholdReached);

        // Método que maneja la carga de más empleados
        private async Task OnRemainingItemsThresholdReached()
        {
            await LoadEmployeesAsync();
        }

        /*
        public EmployeeViewModel()
        {
            // Simulando datos de empleados (puedes reemplazar esto con datos reales)
            Employees = new ObservableCollection<EmployeeEntitie>
            {
                new EmployeeEntitie { Name = "John Doe",  Id = 1, Rfc= "RFC111", DateBirth = DateTime.Now },
                new EmployeeEntitie { Name = "Jane Smith",  Id = 2, Rfc= "RFC222", DateBirth = DateTime.Now },
                new EmployeeEntitie { Name = "Robert Brown",  Id = 3, Rfc= "RFC333", DateBirth = DateTime.Now  },
                new EmployeeEntitie { Name = "Alice Johnson",  Id = 4, Rfc= "RFC444", DateBirth = DateTime.Now  },
                    new EmployeeEntitie { Name = "John Doe 1",  Id = 1, Rfc= "RFC111", DateBirth = DateTime.Now },
                new EmployeeEntitie { Name = "Jane Smith 1",  Id = 2, Rfc= "RFC222", DateBirth = DateTime.Now },
                new EmployeeEntitie { Name = "Robert Brown 1",  Id = 3, Rfc= "RFC333", DateBirth = DateTime.Now  },
                new EmployeeEntitie { Name = "Alice Johnson 1",  Id = 4, Rfc= "RFC444", DateBirth = DateTime.Now  },
                    new EmployeeEntitie { Name = "John Doe 2",  Id = 1, Rfc= "RFC111", DateBirth = DateTime.Now },
                new EmployeeEntitie { Name = "Jane Smith 2",  Id = 2, Rfc= "RFC222", DateBirth = DateTime.Now },
                new EmployeeEntitie { Name = "Robert Brown 2",  Id = 3, Rfc= "RFC333", DateBirth = DateTime.Now  },
                new EmployeeEntitie { Name = "Alice Johnson 2",  Id = 4, Rfc= "RFC444", DateBirth = DateTime.Now  },
                    new EmployeeEntitie { Name = "John Doe 3",  Id = 1, Rfc= "RFC111", DateBirth = DateTime.Now },
                new EmployeeEntitie { Name = "Jane Smith 3",  Id = 2, Rfc= "RFC222", DateBirth = DateTime.Now },
                new EmployeeEntitie { Name = "Robert Brown 3",  Id = 3, Rfc= "RFC333", DateBirth = DateTime.Now  },
                new EmployeeEntitie { Name = "Alice Johnson 3",  Id = 4, Rfc= "RFC444", DateBirth = DateTime.Now  },
                // Agrega más empleados según sea necesario
            };
        }

        */


        // Método para cargar empleados, simulando una llamada a API o base de datos
        public async Task LoadEmployeesAsync()
        {
            if (_isLoading)
                return;

            _isLoading = true;

            // Simulando la carga de datos
            await Task.Delay(1000);  // Simulando un retraso de red o base de datos

            var newEmployees = GetEmployeesForPage(_pageNumber);  // Obtén los empleados de la página actual

            foreach (var employee in newEmployees)
            {
                EmployeList.Add(employee);
            }

            _pageNumber++;

            _isLoading = false;
        }

        // Simulación de la carga de empleados (en un caso real, aquí consultarías una API o base de datos)
        private List<EmployeeEntitie> GetEmployeesForPage(int pageNumber)
        {
            var employees = new List<EmployeeEntitie>();
            for (int i = 0; i < PageSize; i++)
            {
                var employeeNumber = (pageNumber - 1) * PageSize + i + 1;
                employees.Add(new EmployeeEntitie
                {
                    Id = employeeNumber,
                    Name = $"Employee {employeeNumber}",
                    Rfc = $"RFC {employeeNumber}",
                    DateBirth = DateTime.Now
                });
            }

            return employees;
        }
    }
}
