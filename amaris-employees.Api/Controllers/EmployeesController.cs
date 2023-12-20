using Microsoft.AspNetCore.Mvc;
using System.Net;
using AmarisEmployees.Api.Business.Interfaces;
using AmarisEmployees.Api.Business.DTOs;

namespace AmarisEmployees.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesBusiness _bus;

        public EmployeesController(IEmployeesBusiness bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Search all employees.
        /// </summary>
        /// <returns>When search is successfully, List of Employees information and Ok are returned, 
        /// otherwise BadRequest are returned.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<EmployeeDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string?>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var result = await _bus.EmployeesAll();

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    throw new Exception("No records found");
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string?>(null, "no_success", ex.Message);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Search employee that match the filter.
        /// </summary>
        /// <param name="id">Object that content the id employee.</param>
        /// <returns>When search is successfully, Employee information and Ok are returned, 
        /// otherwise BadRequest are returned.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<EmployeeDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string?>))]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var result = await _bus.EmployeeById(id);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    throw new Exception("No records found");
                }
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string?>(null, "no_success", ex.Message);
                return BadRequest(response);
            }
        }
    }
}