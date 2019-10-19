using System;
using Xunit;
using WebApi.Controllers;
using Moq;
using FoodStorage.Services;
using FoodStorage.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTest.Controller
{
    public class FoodStorageControllerTest
    {
        Mock<IPackService> mockPackService;
        private IPackService service;
        private FoodStorageController controller;

        [Fact]
        public void GetPack_PackNotFound_ShouldReturnNotFound()
        {
            // Arrange
            mockPackService = new Mock<IPackService>();
            service = mockPackService.Object;
            controller = new FoodStorageController(service);

            var packId = 1;
            mockPackService.Setup(x => x.GetPack(packId)).Returns<Pack>(null);

            // Act
            var result = controller.GetPack(packId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void GetPack_PackFound_ShouldReturnPack()
        {
            // Arrange
            mockPackService = new Mock<IPackService>();
            service = mockPackService.Object;
            controller = new FoodStorageController(service);

            var packId = 1;
            mockPackService.Setup(x => x.GetPack(packId)).Returns(new Pack() { Id = 1 });

            // Act
            var result = controller.GetPack(packId);

            // Assert
            Assert.IsType<Pack>(result.Value);
            Assert.True(result.Value.Id == packId);
        }
    }
}
