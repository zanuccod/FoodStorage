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
            for (var i = 0; i < 10; i++)
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
            Assert.AreEqual(10, items.Count);
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
            var item = new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 };
            dataModel.InsertPack(item);

            // Act
            item.RemainigItems--;
            dataModel.UpdatePack(item);

            var item2 = dataModel.GetPack(item.Id);

            // Assert
            Assert.AreEqual(5, item2.RemainigItems);
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
