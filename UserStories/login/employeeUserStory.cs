using infrastructure.DataBase;
using interfaces.DataBase;
using Microsoft.Extensions.Configuration;
using Models;
using Models.DTOs;

using common;
using Npgsql.Internal;

namespace UserStories.login
{
    public class employeeUserStory(IMyUnitOfWork unitOfWork) : IemployeeUserStory
    {
        private readonly IMyUnitOfWork unitOfWork = unitOfWork;

        public async Task<Response<List<TblEmpleado>>> GetEmployeList()
        {
            Response<List<TblEmpleado>> result = new Response<List<TblEmpleado>>();
            // check user exist on database
            var RepoEmpleado = unitOfWork.Repository<TblEmpleado>();

            var employeList = await RepoEmpleado.SearchAllAsync().ConfigureAwait(false);

            if (employeList == null)
            {
                result.StatusCode = 400;
                result.Message = "No se cuenta con empleados";

                return result;
            }

            result.StatusCode = 200;
            result.Message = "ok";
            result.Payload = employeList.ToList();

            return result;
        }
    }
}
