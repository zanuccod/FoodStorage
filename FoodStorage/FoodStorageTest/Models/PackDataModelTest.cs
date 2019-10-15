using System;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Collections.Generic;
using FoodStorage.Entities;
using FoodStorage.Models;
using Microsoft.Data.Sqlite;

namespace FoodStorageTest.Models
{
    [TestFixture]
    public class PackDataModelTest
    {
        private DbContextOptions<EntityFrameworkBase<Pack>> options;
        private PackDataModel db;

#pragma warning disable IDE0051 // remove warning of unsed private members
        private IEnumerable<TestCaseData> TestCasesItems()
        {
            yield return new TestCaseData(null, 0);
            yield return new TestCaseData(
                new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 },
                1);
        }

#pragma warning restore IDE0051

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            options = new DbContextOptionsBuilder<EntityFrameworkBase<Pack>>()
                .UseInMemoryDatabase("db_test")
                .Options;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            db = new PackDataModel(options);
        }

        [Test]
        public void AddItem()
        {
            // Arrange
            var item = new Pack() { Name = "tomatoes", TotalItems = 6, RemainigItems = 6 };

            // Act
            db.Add(item);

            // Assert
            Assert.AreEqual(1, db.GetAll().Count);
        }
    }
}
