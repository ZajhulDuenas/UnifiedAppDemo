using Microsoft.Extensions.Configuration;
using Api.Models;
using Models.ClientApi;
using Models.DTOs;

namespace Interfaces.UserStory
{
    public interface ItokenUserStory
    {
        Task<Response<ClientToken>> GetToken(IConfiguration configuration, LoginDto request);
    }
}