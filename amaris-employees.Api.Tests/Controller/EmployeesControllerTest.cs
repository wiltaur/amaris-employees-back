using Microsoft.AspNetCore.Mvc;
using AmarisEmployees.Api.Controllers;
using AmarisEmployees.Api.Business.Interfaces;
using AutoFixture;
using AmarisEmployees.Api.Business.DTOs;

#nullable disable

namespace AmarisEmployees.Api.Tests.Controller
{
    public class EmployeesControllerTest
    {
        private EmployeesController _currentController;
        private readonly Mock<IEmployeesBusiness> _bService = new();
        private Fixture _autodata;

        public EmployeesControllerTest()
        {
            _autodata = new Fixture();
            _autodata.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _autodata.Behaviors.Remove(b));
            _autodata.Behaviors.Add(new OmitOnRecursionBehavior(1));

            _currentController = new(_bService.Object);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        [InlineData(3, 400)]
        public async Task EmployeesAllTest(int index, int expected)
        {
            //Arrange
            var resp = _autodata.Create<ApiResponse<List<EmployeeDto>>>();

            switch (index)
            {
                case 1:
                    _bService
                        .Setup(t => t.EmployeesAll())
                        .Returns(Task.FromResult(resp));
                    break;
                case 2:
                    resp = null;
                    _bService
                        .Setup(t => t.EmployeesAll())
                        .Returns(Task.FromResult(resp));
                    break;
                case 3:
                    _bService
                        .Setup(t => t.EmployeesAll())
                        .Throws(new Exception("Excepción"));
                    break;
            }

            //Act 
            IActionResult response = await _currentController.GetEmployees();
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(expected, result?.StatusCode);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        [InlineData(3, 400)]
        public async Task EmployeeByIdTest(int index, int expected)
        {
            //Arrange
            var resp = _autodata.Create<ApiResponse<EmployeeDto>>();

            switch (index)
            {
                case 1:
                    _bService
                        .Setup(t => t.EmployeeById(It.IsAny<int>()))
                        .Returns(Task.FromResult(resp));
                    break;
                case 2:
                    resp = null;
                    _bService
                        .Setup(t => t.EmployeeById(It.IsAny<int>()))
                        .Returns(Task.FromResult(resp));
                    break;
                case 3:
                    _bService
                        .Setup(t => t.EmployeeById(It.IsAny<int>()))
                        .Throws(new Exception("Excepción"));
                    break;
            }

            //Act 
            IActionResult response = await _currentController.GetEmployeeById(1);
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(expected, result?.StatusCode);
        }
    }
}