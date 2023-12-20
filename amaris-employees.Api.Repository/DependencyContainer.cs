using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AmarisEmployees.Api.Repository.Implements;
using AmarisEmployees.Api.Repository.Interfaces;

namespace AmarisEmployees.Api.Repository
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IEmployeesDataAccess, EmployeesDataAccess>();
            return services;
        }
    }
}