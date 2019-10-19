using System;
using System.Threading.Tasks;
using FoodStorage.Entities;
using FoodStorage.Models;
using FoodStorage.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace FoodStorageTest.Services
{
    [TestFixture]
    public class PackServiceTest
    {
        private PackService service;

        [SetUp]
        public void BeforeEachTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var model = new EFDataModel(options);
            service = new PackService(model);
        }

        [TestCase(6, 6, true)]
        [TestCase(6, 5, false)]
        public async Task IsPackCompleteTest(int totalItems, int remainingItems, bool expectedResult)
        {
            // Arrange
            var item = new Pack()
            {
                TotalItems = totalItems,
                RemainigItems = remainingItems
            };
            await service.AddPack(item);

            // Act
            var result = await service.IsPackComplete(item);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task RemoveItemFromPackTest_MoreItemsToRemove_ShouldDecrementRemainigsItems()
        {
            // Arrange
            var item = new Pack()
            {
                TotalItems = 6,
                RemainigItems = 2
            };
            await service.AddPack(item);

            // Act
            await service.RemoveItemFromPack(item);

            // Assert
            Assert.AreEqual(1, service.GetPack(item.Id).Result.RemainigItems);
        }

        [Test]
        public async Task RemoveItemFromPackTest_OneItemToRemove_ShouldRemovePack()
        {
            // Arrange
            var item = new Pack()
            {
                TotalItems = 6,
                RemainigItems = 1
            };
            await service.AddPack(item);

            // Act
            await service.RemoveItemFromPack(item);

            // Assert
            Assert.Null(service.GetPack(item.Id).Result);
        }
    }
}
