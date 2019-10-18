using System;
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
        public void IsPackCompleteTest(int totalItems, int remainingItems, bool expectedResult)
        {
            // Arrange
            var item = new Pack()
            {
                TotalItems = totalItems,
                RemainigItems = remainingItems
            };
            service.AddPack(item);

            // Act
            var result = service.IsPackComplete(item.Id);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void RemoveItemFromPackTest_MoreItemsToRemove_ShouldDecrementRemainigsItems()
        {
            // Arrange
            var item = new Pack()
            {
                TotalItems = 6,
                RemainigItems = 2
            };
            service.AddPack(item);

            // Act
            service.RemoveItemFromPack(item.Id);

            // Assert
            Assert.AreEqual(1, service.GetPack(item.Id).RemainigItems);
        }

        [Test]
        public void RemoveItemFromPackTest_OneItemToRemove_ShouldRemovePack()
        {
            // Arrange
            var item = new Pack()
            {
                TotalItems = 6,
                RemainigItems = 1
            };
            service.AddPack(item);

            // Act
            service.RemoveItemFromPack(item.Id);

            // Assert
            Assert.Null(service.GetPack(item.Id));
        }
    }
}
