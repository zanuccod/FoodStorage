using System;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FoodStorage.Entities;
using FoodStorage.Models;

namespace FoodStorageTest.Models
{
    [TestFixture]
    public class EFDataModelTest
    {
        private DbContextOptions options;
        private EFDataModel dataModel;

        [SetUp]
        public void BeforeEachTest()
        {
            options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            dataModel = new EFDataModel(options);
        }

        [Test]
        public void InsertPack()
        {
            // Arrange
            var item = new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 };

            // Act
            dataModel.InsertPack(item);

            // Assert
            Assert.AreEqual(1, dataModel.GetAllPacks().Count);
        }

        [Test]
        public void GetAllPacks()
        {
            // Arrange
            var itemCount = 10;
            for (var i = 0; i < itemCount; i++)
            {
                dataModel.InsertPack(new Pack()
                {
                    Name = $"tomatoes_{i}",
                    TotalItems = i,
                    RemainigItems = i
                });
            }

            // Act
            var items = dataModel.GetAllPacks();

            // Assert
            Assert.AreEqual(itemCount, items.Count);
        }

        [Test]
        public void GetPack()
        {
            // Arrange
            var item = new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 };
            dataModel.InsertPack(item);

            // Act
            var item2 = dataModel.GetPack(item.Id);

            // Assert
            Assert.True(item.Equals(item2));
        }

        [Test]
        public void UpdatePack()
        {
            // Arrange
            var newName = "beer";
            var item = new Pack() { Name = "tomatoes" };
            dataModel.InsertPack(item);

            // Act
            item.Name = newName;
            dataModel.UpdatePack(item);

            var item2 = dataModel.GetPack(item.Id);

            // Assert
            Assert.AreEqual(newName, item2.Name);
        }

        [Test]
        public void DeletePack()
        {
            // Arrange
            var item = new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 };
            dataModel.InsertPack(item);

            // Act
            dataModel.DeletePack(item.Id);

            // Assert
            Assert.AreEqual(0, dataModel.GetAllPacks().Count);
        }
    }
}
