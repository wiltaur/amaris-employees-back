using AmarisEmployees.Api.Business.DTOs;

namespace AmarisEmployees.Api.Business.Interfaces
{
    public interface IEmployeesBusiness
    {
        Task<ApiResponse<EmployeeDto>> EmployeeById(int id);
        Task<ApiResponse<List<EmployeeDto>>> EmployeesAll();
    }
}