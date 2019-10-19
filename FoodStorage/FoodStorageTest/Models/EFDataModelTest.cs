using System;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FoodStorage.Entities;
using FoodStorage.Models;
using System.Threading.Tasks;

namespace FoodStorageTest.Models
{
    [TestFixture]
    public class EFDataModelTest
    {
        private EFDataModel dataModel;

        [SetUp]
        public void BeforeEachTest()
        {
            var options = new DbContextOptionsBuilder()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .Options;
            dataModel = new EFDataModel(options);
        }

        [Test]
        public async Task InsertPack()
        {
            // Arrange
            var item = new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 };

            // Act
            await dataModel.InsertPack(item);

            // Assert
            Assert.AreEqual(1, dataModel.GetPackList().Result.Count);
        }

        [Test]
        public async Task GetAllPacks()
        {
            // Arrange
            var itemCount = 10;
            for (var i = 0; i < itemCount; i++)
            {
                await dataModel.InsertPack(new Pack()
                {
                    Name = $"tomatoes_{i}",
                    TotalItems = i,
                    RemainigItems = i
                });
            }

            // Act
            var items = await dataModel.GetPackList();

            // Assert
            Assert.AreEqual(itemCount, items.Count);
        }

        [Test]
        public async Task GetPack()
        {
            // Arrange
            var item = new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 };
            await dataModel.InsertPack(item);

            // Act
            var item2 = await dataModel.GetPack(item.Id);

            // Assert
            Assert.True(item.Equals(item2));
        }

        [Test]
        public async Task UpdatePack()
        {
            // Arrange
            var newName = "beer";
            var item = new Pack() { Name = "tomatoes" };
            await dataModel.InsertPack(item);

            // Act
            item.Name = newName;
            await dataModel.UpdatePack(item);

            var item2 = await dataModel.GetPack(item.Id);

            // Assert
            Assert.AreEqual(newName, item2.Name);
        }

        [Test]
        public async Task DeletePack()
        {
            // Arrange
            var item = new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 };
            await dataModel.InsertPack(item);

            // Act
            await dataModel.DeletePack(item.Id);

            // Assert
            Assert.AreEqual(0, dataModel.GetPackList().Result.Count);
        }
    }
}
