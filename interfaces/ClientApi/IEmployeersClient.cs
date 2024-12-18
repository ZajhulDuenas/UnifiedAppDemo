using Api.Models;
using Models.ClientApi;
using Models.DTOs;

namespace Front.Infrastructure.ClientApi
{
    public interface IEmployeersClient
    {
        string baseEndPoint { get; set; }
        ClientToken? ClientToken { get; set; }

        Task<Response<EmployeeDto>> AddEmployee(EmployeeDto request);
        Task<Response<EmployeeDto>> DeleteEmployee(int id);
        Task<Response<byte[]>> ExportData();
        Task<Response<List<EmployeeDto>>> GetEmployeersByIndex(int index = 1, int pageSize = 10);
        Task<object> ImportData(MultipartFormDataContent content);
        Task<Response<EmployeeDto>> ModifyEmployee(EmployeeDto request);
    }
}