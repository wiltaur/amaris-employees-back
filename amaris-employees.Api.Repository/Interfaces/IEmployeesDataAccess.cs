namespace AmarisEmployees.Api.Repository.Interfaces
{
    public interface IEmployeesDataAccess
    {
        Task<string> EmployeeById(int id);
        Task<string> EmployeesAll();
    }
}