using Api.Models;
using Models.DataBase;
using Models.DTOs;

namespace UserStories.login
{
    public interface IemployeeUserStory
    {
        Task<Response<TblEmpleado>> AddEmployee(EmployeeDto input);
        Task<Response<TblEmpleado>> DeleteEmployee(EmployeeIdDto request);
        Task<Response<FileStreamDto>> ExportData();
        Task<Response<TblEmpleado>> GetEmployeById(EmployeeIdDto request);
        Task<Response<List<TblEmpleado>>> GetEmployeList();
        Task<Response<List<TblEmpleado>>> GetEmployeList(PaginationDto request);
        Task<Response> ImportData(MemoryStream stream);
        Task<Response<TblEmpleado>> ModifyEmployee(Employee2RequestDto request);
    }
}