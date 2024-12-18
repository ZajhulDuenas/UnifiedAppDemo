using Api.Models;
using Api.Models.Request;
using Models.DataBase;
using Models.DTOs;

namespace UserStories.login
{
    public interface IemployeeUserStory
    {
        Task<Response<TblEmpleado>> AddEmployee(EmployeeRequest input);
        Task<Response<TblEmpleado>> DeleteEmployee(EmployeeIdRequest request);
        Task<Response<FileStreamDto>> ExportData();
        Task<Response<TblEmpleado>> GetEmployeById(EmployeeIdRequest request);
        Task<Response<List<TblEmpleado>>> GetEmployeList();
        Task<Response<List<TblEmpleado>>> GetEmployeList(PaginationRequest request);
        Task<Response> ImportData(MemoryStream stream);
        Task<Response<TblEmpleado>> ModifyEmployee(Employee2Request request);
    }
}