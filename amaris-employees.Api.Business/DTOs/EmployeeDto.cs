using System.Text.Json.Serialization;
#nullable disable

namespace AmarisEmployees.Api.Business.DTOs
{
    public partial class EmployeeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("employee_name")]
        public string Name { get; set; }

        [JsonPropertyName("employee_salary")]
        public decimal Salary { get; set; }


        [JsonPropertyName("employee_anual_salary")]
        public decimal? AnualSalary { get; set; }

        [JsonPropertyName("employee_age")]
        public int Age { get; set; }

        [JsonPropertyName("profile_image")]
        public string Image { get; set; }
    }
}