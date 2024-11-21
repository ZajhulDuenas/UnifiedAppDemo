using Microsoft.AspNetCore.Components;

namespace WepApp.Pages
{

    public partial class Home
    {
        [Inject] protected AuthService AuthService { get; set; }
        protected string Name { get; set; }
        private bool isAuthenticated { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (AuthService != null) {
                isAuthenticated = await AuthService.CheckIsAuthenticated();
                if (isAuthenticated)
                {
                    var info = await AuthService.GetInfoUser();

                    Name = info.Name;
                }
            }

            
        }

    }
}
