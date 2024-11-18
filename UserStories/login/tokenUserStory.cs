using interfaces.DataBase;
using Models;
using Models.DTOs;
using common;
using infrastructure.jwt;
using Microsoft.Extensions.Configuration;
using Interfaces.UserStory;
using Models.ClientApi;
using Models.DataBase;


namespace UserStories.login
{
    public class tokenUserStory(
        IMyUnitOfWork unitOfWork, 
        IMyTokenGenerator tokenGenerator) : ItokenUserStory
    {
        private readonly IMyUnitOfWork unitOfWork = unitOfWork;
        private readonly IMyTokenGenerator tokenGenerator = tokenGenerator;
        public async Task<Response<ClientToken>> GetToken(IConfiguration configuration, LoginDto request)
        {

            // check user exist on database
            var RepUser = unitOfWork.Repository<CatUsuario>();

            var user = await RepUser.FirstOrDefaultAsync(x => (x.Usuario == request.Username && x.Password == request.Password)).ConfigureAwait(false);

            if (user == null)
            {
                return new Response<ClientToken>() { StatusCode = 400, Message = "user no identificado" };
            }

            var RepRol = unitOfWork.Repository<CatRol>();

            var Rol = await RepRol.FirstOrDefaultAsync(x => x.IdRol == user.IdRol);

            Dictionary<string, bool> claim_list = new Dictionary<string, bool>() {

                { "Crear",Rol.Crear.Value },
                { "Actualizar",Rol.Actualizar.Value },
                { "Eliminar",Rol.Eliminar.Value },
                { "Listar",Rol.Listar.Value },
                { "Importar",Rol.Importar.Value },
                { "Exportar",Rol.Exportar.Value },

            };

            var jwtKey = configuration["Jwt:Key"];
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtAudience = configuration["Jwt:Audience"];

            var token = tokenGenerator.GenerateStringToken(user.IdUsuario.ToString(), user.Usuario, user.Nombre,claim_list, jwtKey, jwtIssuer, jwtAudience);

            return new Response<ClientToken>() { StatusCode = 200, Message = "OK", Payload = new ClientToken() { Token = token } };
           // return null;
        }
    }
}
