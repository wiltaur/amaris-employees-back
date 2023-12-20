using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AmarisEmployees.Api.Models.Contexts;

namespace AmarisEmployees.Api.Models
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddConnectionDb(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName)
        {
            services.AddDbContext<AmarisEmployeesContext>(options =>
            options.UseSqlServer(configuration
            .GetConnectionString(connectionStringName)));
            return services;
        }
    }
}