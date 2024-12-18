using Microsoft.Extensions.Configuration;
using Api.Models;
using Models.ClientApi;
using Api.Models.Request;

namespace Interfaces.UserStory
{
    public interface ItokenUserStory
    {
        Task<Response<ClientToken>> GetToken(IConfiguration configuration, LoginRequest request);
    }
}