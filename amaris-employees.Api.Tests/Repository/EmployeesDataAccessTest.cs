using Microsoft.EntityFrameworkCore;
using AutoFixture;
using Microsoft.Extensions.Configuration;
using AmarisEmployees.Api.Repository.Implements;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace AmarisEmployees.Api.Tests.Repository
{
    public class EmployeesDataAccessTest
    {
        private readonly Mock<HttpMessageHandler> _httpClient = new();
        private readonly Mock<IConfiguration> _config = new();
        Fixture _autoData;

        private EmployeesDataAccess? _service;

        public EmployeesDataAccessTest()
        {
            _autoData = new Fixture();
        }

        [Theory]
        [InlineData(1, "test")]
        [InlineData(2, "Error")]
        public void EmployeesAllTest(int index, string expected)
        {
            //Arrange
            // Configurar el comportamiento esperado del HttpClient mock
            var result = new
            {
                test = @"test"
            };

            //Tranform it to Json object
            var expectedData = JsonSerializer.Serialize(result);

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedData)
            };

            switch (index)
            {
                case 1:
                    _httpClient.Protected()
                        .Setup<Task<HttpResponseMessage>>("SendAsync",
                          ItExpr.IsAny<HttpRequestMessage>(),
                          ItExpr.IsAny<CancellationToken>())
                        .ReturnsAsync(httpResponseMessage);
                    break;
                case 2:
                    _httpClient.Protected()
                        .Setup<Task<HttpResponseMessage>>("SendAsync",
                            ItExpr.IsAny<HttpRequestMessage>(),
                            ItExpr.IsAny<CancellationToken>())
                        .Throws(new Exception("Error"));
                    break;
            }
            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("https://example.com/api/stuff");
            _config.Setup(s => s.GetSection(It.IsAny<string>())).Returns(oneSectionMock.Object);
            var client = new HttpClient(_httpClient.Object);
            _service = new(client, _config.Object);

            //Act
            var response = _service.EmployeesAll();

            //Assert
            Assert.NotNull(response);
            Assert.Contains(expected, response.Result);
        }

        [Theory]
        [InlineData(1, "test")]
        [InlineData(2, "Error")]
        public void EmployeeByIdTest(int index, string expected)
        {
            //Arrange
            // Configurar el comportamiento esperado del HttpClient mock
            var result = new
            {
                test = @"test"
            };

            //Tranform it to Json object
            var expectedData = JsonSerializer.Serialize(result);

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedData)
            };

            switch (index)
            {
                case 1:
                    _httpClient.Protected()
                        .Setup<Task<HttpResponseMessage>>("SendAsync",
                          ItExpr.IsAny<HttpRequestMessage>(),
                          ItExpr.IsAny<CancellationToken>())
                        .ReturnsAsync(httpResponseMessage);
                    break;
                case 2:
                    _httpClient.Protected()
                        .Setup<Task<HttpResponseMessage>>("SendAsync",
                            ItExpr.IsAny<HttpRequestMessage>(),
                            ItExpr.IsAny<CancellationToken>())
                        .Throws(new Exception("Error"));
                    break;
            }
            var oneSectionMock = new Mock<IConfigurationSection>();
            oneSectionMock.Setup(s => s.Value).Returns("https://example.com/api/stuff");
            _config.Setup(s => s.GetSection(It.IsAny<string>())).Returns(oneSectionMock.Object);
            var client = new HttpClient(_httpClient.Object);
            _service = new(client, _config.Object);

            //Act
            var response = _service.EmployeeById(1);

            //Assert
            Assert.NotNull(response);
            Assert.Contains(expected, response.Result);
        }
    }
}