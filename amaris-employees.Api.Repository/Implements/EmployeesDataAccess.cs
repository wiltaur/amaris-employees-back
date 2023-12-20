#nullable disable

using AmarisEmployees.Api.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AmarisEmployees.Api.Repository.Implements
{
    public class EmployeesDataAccess : IEmployeesDataAccess
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EmployeesDataAccess(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> EmployeesAll()
        {
            return _ = await GetDataString(_configuration.GetSection("AllConnections:AllEmployees").Value);
        }

        public async Task<string> EmployeeById(int id)
        {
            return _ = await GetDataString(_configuration.GetSection("AllConnections:EmployeeById").Value + id);
        }

        private async Task<string> GetDataString(string url)
        {
            try
            {
                HttpRequestMessage requestMessage = new()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };
                CancellationToken cancellationToken = new();
                HttpResponseMessage response;

                response = await _httpClient.SendAsync(requestMessage, cancellationToken);
                return _ = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return _configuration.GetSection("ErrorConsulta").Value + ": " + ex.ToString();
            }
        }
    }
}