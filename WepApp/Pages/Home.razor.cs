using Front.Infrastructure.ClientApi;
using Microsoft.AspNetCore.Components;
using WepApp.Models;
using WepApp.Services;

namespace WepApp.Pages
{

    public partial class Home
    {
        [Inject] protected AuthService AuthService { get; set; }
        [Inject] protected VarsService VarsService { get; set; }
        protected string Name { get; set; }
        private bool isAuthenticated { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            if (AuthService != null) {
                isAuthenticated = await AuthService.IsAuthenticated();

                if (isAuthenticated)
                {
                    var info = await VarsService.ExtractObject<UserModel>("UserModel");

                    Name = info.Name;
                }
            }

            
        }

    }
}
