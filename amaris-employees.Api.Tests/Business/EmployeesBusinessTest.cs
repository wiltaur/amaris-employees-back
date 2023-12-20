using AmarisEmployees.Api.Business.Implements;
using AmarisEmployees.Api.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using AutoFixture;
using AmarisEmployees.Api.Business.DTOs;
using System.Text.Json;

#nullable disable

namespace AmarisEmployees.Api.Tests.Business
{
    public class EmployeesBusinessTest
    {
        private EmployeesBusiness _currentBusiness;
        private readonly Mock<IEmployeesDataAccess> _bService = new();
        private readonly Mock<IConfiguration> _config = new();
        private Fixture _autodata;

        public EmployeesBusinessTest()
        {
            _autodata = new Fixture();
            _autodata.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _autodata.Behaviors.Remove(b));
            _autodata.Behaviors.Add(new OmitOnRecursionBehavior(1));

            _currentBusiness = new(_bService.Object, _config.Object);
        }

        [Fact]
        public async Task EmployeesAllTest()
        {
            //Arrange
            var result = _autodata.Create<ApiResponse<List<EmployeeDto>>>();

            string resultJson = JsonSerializer.Serialize(result);

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Error");
            _config.Setup(s => s.GetSection(It.IsAny<string>())).Returns(oneSectionMock.Object);

            _bService
                .Setup(t => t.EmployeesAll())
                .Returns(Task.FromResult(resultJson));

            //Act 
            var response = await _currentBusiness.EmployeesAll();

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task EmployeesAllNullTest()
        {
            //Arrange
            string result = null;

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Error");
            _config.Setup(s => s.GetSection(It.IsAny<string>())).Returns(oneSectionMock.Object);

            _bService
                .Setup(t => t.EmployeesAll())
                .Returns(Task.FromResult(result));

            //Act 
            var response = await _currentBusiness.EmployeesAll();

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task EmployeeByIdTest()
        {
            //Arrange
            var result = _autodata.Create<ApiResponse<EmployeeDto>>();

            string resultJson = JsonSerializer.Serialize(result);

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Error");
            _config.Setup(s => s.GetSection(It.IsAny<string>())).Returns(oneSectionMock.Object);

            _bService
                .Setup(t => t.EmployeeById(It.IsAny<int>()))
                .Returns(Task.FromResult(resultJson));

            //Act 
            var response = await _currentBusiness.EmployeeById(1);

            //Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async Task EmployeeByIdNullTest()
        {
            //Arrange
            string result = null;

            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("Error");
            _config.Setup(s => s.GetSection(It.IsAny<string>())).Returns(oneSectionMock.Object);

            _bService
                .Setup(t => t.EmployeeById(It.IsAny<int>()))
                .Returns(Task.FromResult(result));

            //Act 
            var response = await _currentBusiness.EmployeeById(1);

            //Assert
            Assert.Null(response);
        }
    }
}