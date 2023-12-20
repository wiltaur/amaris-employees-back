using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AmarisEmployees.Api.Business.Implements;
using AmarisEmployees.Api.Business.Interfaces;
using AmarisEmployees.Api.Repository;

namespace AmarisEmployees.Api.Business
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddBusinessRepos(
            this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddScoped<IEmployeesBusiness, EmployeesBusiness>();
            return services;
        }
    }
}