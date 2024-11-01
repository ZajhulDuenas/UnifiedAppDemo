using infrastructure.DataBase;
using Microsoft.Extensions.Configuration;
using Models;
using Models.DTOs;

namespace UserStories.login
{
    public interface IemployeeUserStory
    {
        Task<Response<List<TblEmpleado>>> GetEmployeList();
    }
}