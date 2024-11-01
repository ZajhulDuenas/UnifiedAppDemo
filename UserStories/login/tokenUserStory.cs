using infrastructure.DataBase;
using interfaces.DataBase;
using Models;
using Models.ClientApi;
using Models.DTOs;
using common;


namespace UserStories.login
{
    public class tokenUserStory(IMyUnitOfWork unitOfWork)
    {
        private readonly IMyUnitOfWork unitOfWork = unitOfWork;
        public async Task<Response<ClientToken>> GetToken(LoginDto request)
        {

            // check user exist on database
            var RepUser = unitOfWork.Repository<CatUsuario>();
           
            var user = await RepUser.FirstOrDefaultAsync(x => (x.Usuario == request.Username && x.Password == request.Password)).ConfigureAwait(false);

            if ( user == null )
            {
                return new Response<ClientToken>() { StatusCode = 400, Message ="user no identificado" };
            }

            var RepRol = unitOfWork.Repository<CatRol>();

            var Rol = RepRol.FirstOrDefaultAsync(x => x.IdRol == user.IdRol);


            return null;
        }
    }
}
