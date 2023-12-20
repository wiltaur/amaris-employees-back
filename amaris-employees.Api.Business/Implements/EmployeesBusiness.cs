using Microsoft.Extensions.Configuration;
using AmarisEmployees.Api.Business.DTOs;
using AmarisEmployees.Api.Repository.Interfaces;
using System.Text.Json;
using AmarisEmployees.Api.Business.Interfaces;

#nullable disable

namespace AmarisEmployees.Api.Business.Implements
{
    public class EmployeesBusiness : IEmployeesBusiness
    {
        private readonly IEmployeesDataAccess _dataAccess;
        private readonly IConfiguration _config;

        public EmployeesBusiness(IEmployeesDataAccess da, IConfiguration config)
        {
            _dataAccess = da;
            _config = config;
        }

        public async Task<ApiResponse<List<EmployeeDto>>> EmployeesAll()
        {
            string resp = await _dataAccess.EmployeesAll();
            if (resp != null && !resp.Contains(_config.GetSection("ErrorConsulta").Value))
            {
                var result = JsonSerializer.Deserialize<ApiResponse<List<EmployeeDto>>>(resp);
                List<EmployeeDto> employeesTemp = result.Data;
                SetAnualSalary(ref employeesTemp);
                result.Data = employeesTemp;
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<ApiResponse<EmployeeDto>> EmployeeById(int id)
        {
            string resp = await _dataAccess.EmployeeById(id);
            if (resp != null && !resp.Contains(_config.GetSection("ErrorConsulta").Value))
            {
                var result = JsonSerializer.Deserialize<ApiResponse<EmployeeDto>>(resp);
                
                if (result.Data != null)
                {
                    List<EmployeeDto> employees = new();
                    employees.Add(result.Data);
                    SetAnualSalary(ref employees);
                    result.Data = employees[0];
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        private void SetAnualSalary(ref List<EmployeeDto> employees)
        {
            if (employees.Any())
            {
                foreach (var employee in employees)
                {
                    employee.AnualSalary = employee.Salary * 12;
                }
            }
        }
    }
}