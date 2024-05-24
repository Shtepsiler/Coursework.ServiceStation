using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using PARTS.BLL.DTOs.Requests;
using PARTS.BLL.DTOs.Responses;
using PARTS.BLL.Services.Interaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ClientPartAPI.Controllers;
using Newtonsoft.Json;

namespace ServiceStationTests.Tests.APIControllersTests.PartsAPITests
{
        public class VehicleControllerTests
        {
            private readonly Mock<ILogger<VehicleController>> _loggerMock;
            private readonly Mock<IDistributedCache> _cacheMock;
            private readonly Mock<IVehicleService> _serviceMock;
            private readonly VehicleController _controller;

            public VehicleControllerTests()
            {
                _loggerMock = new Mock<ILogger<VehicleController>>();
                _cacheMock = new Mock<IDistributedCache>();
                _serviceMock = new Mock<IVehicleService>();
                _controller = new VehicleController(_loggerMock.Object, _cacheMock.Object, _serviceMock.Object);
            }

            [Fact]
            public async Task GetAllAsync_ReturnsOkResult_WithVehicleList()
            {
                // Arrange
                var vehicles = new List<VehicleResponse>
            {
                new VehicleResponse { Id = Guid.NewGuid(), FullModelName = "Model X" },
                new VehicleResponse { Id = Guid.NewGuid(), FullModelName = "Model Y" }
            };
                _serviceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(vehicles);

                // Act
                var result = await _controller.GetAllAsync();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnValue = Assert.IsType<List<VehicleResponse>>(okResult.Value);
                Assert.Equal(vehicles.Count, returnValue.Count);
            }

            [Fact]
            public async Task GetByIdAsync_ReturnsOkResult_WithVehicle()
            {
                // Arrange
                var vehicleId = Guid.NewGuid();
                var vehicle = new VehicleResponse { Id = vehicleId, FullModelName = "Model X" };
                _serviceMock.Setup(service => service.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

                // Act
                var result = await _controller.GetByIdAsync(vehicleId);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnValue = Assert.IsType<VehicleResponse>(okResult.Value);
                Assert.Equal(vehicleId, returnValue.Id);
            }

            [Fact]
            public async Task GetByIdAsync_ReturnsNotFound_WhenVehicleDoesNotExist()
            {
                // Arrange
                var vehicleId = Guid.NewGuid();
                _serviceMock.Setup(service => service.GetByIdAsync(vehicleId)).ReturnsAsync((VehicleResponse)null);

                // Act
                var result = await _controller.GetByIdAsync(vehicleId);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }

            [Fact]
            public async Task PostAsync_ReturnsCreated_WhenVehicleIsValid()
            {
                // Arrange
                var vehicleRequest = new VehicleRequest { FullModelName = "Model X" };

                // Act
                var result = await _controller.PostAsync(vehicleRequest);

                // Assert
                Assert.IsType<CreatedResult>(result);
            }

            [Fact]
            public async Task PostAsync_ReturnsBadRequest_WhenVehicleIsNull()
            {
                // Act
                var result = await _controller.PostAsync(null);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Обєкт Vehicle є null", badRequestResult.Value);
            }

            [Fact]
            public async Task UpdateAsync_ReturnsNoContent_WhenVehicleIsUpdated()
            {
                // Arrange
                var vehicleRequest = new VehicleRequest { FullModelName = "Model X" };

                // Act
                var result = await _controller.UpdateAsync(vehicleRequest);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async Task UpdateAsync_ReturnsBadRequest_WhenVehicleIsNull()
            {
                // Act
                var result = await _controller.UpdateAsync(null);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Обєкт Vehicle є null", badRequestResult.Value);
            }

            [Fact]
            public async Task DeleteByIdAsync_ReturnsNoContent_WhenVehicleIsDeleted()
            {
                // Arrange
                var vehicleId = Guid.NewGuid();
                var vehicle = new VehicleResponse { Id = vehicleId, FullModelName = "Model X" };
                _serviceMock.Setup(service => service.GetByIdAsync(vehicleId)).ReturnsAsync(vehicle);

                // Act
                var result = await _controller.DeleteByIdAsync(vehicleId);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async Task DeleteByIdAsync_ReturnsNotFound_WhenVehicleDoesNotExist()
            {
                // Arrange
                var vehicleId = Guid.NewGuid();
                _serviceMock.Setup(service => service.GetByIdAsync(vehicleId)).ReturnsAsync((VehicleResponse)null);

                // Act
                var result = await _controller.DeleteByIdAsync(vehicleId);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }


