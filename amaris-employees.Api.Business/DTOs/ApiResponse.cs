using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AmarisEmployees.Api.Business.DTOs
{
    /// <summary>
    /// Generic responses for all requests.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public ApiResponse(T data, string status, string message)
        {
            Data = data;
            Status = status;
            Message = message;
        }

        [JsonPropertyName("data")]
        public T Data { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}