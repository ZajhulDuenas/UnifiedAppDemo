using Api.Models;
using Models.ClientApi;

namespace Front.Infrastructure.ClientApi
{
    public interface IAuthServiceClient
    {
        ClientToken? ClientToken { get; set; }

        string baseEndPoint{ get; set; }
        string password { get; set; }
        string userName { get; set; }

        Task<Response<ClientToken>> OnBasicGetToken(string user, string pass);
    }
}