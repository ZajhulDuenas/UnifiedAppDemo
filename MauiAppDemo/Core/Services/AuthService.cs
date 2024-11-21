
using Front.Infrastructure.ClientApi;
using WepApp.Core.Entities;
using common;

namespace MauiAppDemo.Core.Services
{

    public class AuthService
    {
        private IAuthServiceClient _tokenServiceClient { get; set; } = default!;

        private string tagtoken = "authToken";
        private string tagUserModel = "UserModel";

        public AuthService(IAuthServiceClient tokenServiceClient) 
        {
            _tokenServiceClient = tokenServiceClient;

        }

        public async Task<bool> LoginAsync(string user, string pass)
        {
            bool result = false;

            var response = await _tokenServiceClient.OnBasicGetToken(user, pass);

            if (response.Success)
            {

                var token = response.Payload.Token;

                // extraer ckaims
                var claims_list = JwtTokenService.GetClaimsFromJwt(token);

                var LocalUser = new UserEntitie();

                LocalUser.Id = int.Parse(claims_list.FirstOrDefault(c => c.Type == "userId").Value);
                LocalUser.Name = claims_list.FirstOrDefault(c => c.Type == "realName").Value;
                LocalUser.ReadList = claims_list.FirstOrDefault(c => c.Type == "Listar").Value == "True";
                LocalUser.AddUser = claims_list.FirstOrDefault(c => c.Type == "Crear").Value == "True";
                LocalUser.ModifyUser = claims_list.FirstOrDefault(c => c.Type == "Actualizar").Value == "True";
                LocalUser.DeleteUser = claims_list.FirstOrDefault(c => c.Type == "Eliminar").Value == "True";
                LocalUser.ImportList = claims_list.FirstOrDefault(c => c.Type == "Importar").Value == "True";
                LocalUser.ExportList = claims_list.FirstOrDefault(c => c.Type == "Exportar").Value == "True";

                // save token 
                await SecureStorage.SetAsync(tagtoken, token);

                // save user info
                await SecureStorage.SetAsync(tagUserModel, LocalUser.SerializeJson());

                result = true;
            }

            return result;
        }

        public async Task<bool> CheckIsAuthenticated()
        {
            var token = await SecureStorage.GetAsync(tagtoken);

            if (string.IsNullOrEmpty(token)) { return false; }

            var result = !token.IsJwtExpired();

            return result;
        }


        public async Task<UserEntitie> GetInfoUser()
        {
            if (await CheckIsAuthenticated())
            {

                var serailiseInfo = await SecureStorage.GetAsync(tagUserModel);

                var user = serailiseInfo.DeserializeJson<UserEntitie>();

                // var token = await _VarsService.ExtractObject<UserEntitie>(tagUserModel);

                return user;
            }
            return null;

        }

        public async Task<string> GetToken()
        {
            if (await CheckIsAuthenticated())
            {

                var token = await SecureStorage.GetAsync(tagtoken);

                return token;
            }
            return "";

        }

    }
}
