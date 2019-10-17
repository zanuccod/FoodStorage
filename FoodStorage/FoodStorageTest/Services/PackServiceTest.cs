using System;
using FoodStorage.Entities;
using FoodStorage.Models;
using FoodStorage.Services;
using Moq;
using NUnit.Framework;

namespace FoodStorageTest.Services
{
    [TestFixture]
    public class PackServiceTest
    {
        Mock<IPackDataModel> mockModel;
        private IPackDataModel model;
        private PackService service;

        [SetUp]
        public void BeforeEachTest()
        {
            mockModel = new Mock<IPackDataModel>();
            model = mockModel.Object;
            service = new PackService(model);
        }

        [TestCase(6, 6, true)]
        [TestCase(6, 5, false)]
        public void IsPackCompleteTest(int totalItems, int remainingItems, bool expectedResult)
        {
            // Arrange
            var item = new Pack()
            {
                Id = 1,
                TotalItems = totalItems,
                RemainigItems = remainingItems
            };

            mockModel.Setup(x => x.GetPack(item.Id)).Returns(item);

            // Act
            var result = service.IsPackComplete(item.Id);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void RemoveItemFromPackTest_MoreItemsToRemove_ShouldCallUpdatePackMethod()
        {
            // Arrange
            mockModel.Setup(x => x.GetPack(It.IsAny<long>())).Returns(new Pack() { RemainigItems = 2 });
            mockModel.Setup(x => x.UpdatePack(It.IsAny<Pack>()));
            mockModel.Setup(x => x.DeletePack(It.IsAny<long>())).Throws(new Exception("In this case, the DeletePack should not be called."));

            // Act
            service.RemoveItemFromPack(1);
        }

        [Test]
        public void RemoveItemFromPackTest_OneItemToRemove_ShouldCallDeletePackMethod()
        {
            // Arrange
            mockModel.Setup(x => x.GetPack(It.IsAny<long>())).Returns(new Pack() { RemainigItems = 1 });
            mockModel.Setup(x => x.DeletePack(It.IsAny<long>()));
            mockModel.Setup(x => x.UpdatePack(It.IsAny<Pack>())).Throws(new Exception("In this case, the UpdatePack should not be called."));

            // Act
            service.RemoveItemFromPack(1);
        }

        /*
        [Test]
        public void RemoveItemFromPackTest_MoreItemsToRemove_ShouldCallUpdatePackMethod()
        {
            // Arrange
            var item = new Pack()
            {
                Id = 1,
                TotalItems = 6,
                RemainigItems = 6
            };
            var item2 = new Pack()
            {
                Id = 1,
                TotalItems = 6,
                RemainigItems = 6
            };
            mockModel.Setup(x => x.GetPack(item.Id)).Returns(item);
            mockModel.Setup(x => x.UpdatePack(item2));
            mockModel.Setup(x => x.DeletePack(item.Id)).Throws(new Exception("In this case, the DeletePack should not be called."));

            // Act
            service.RemoveItemFromPack(item.Id);
        }

        [Test]
        public void RemoveItemFromPackTest_OneItemToRemove_ShouldCallDeletePackMethod()
        {
            // Arrange
            var item = new Pack()
            {
                Id = 1,
                TotalItems = 6,
                RemainigItems = 1
            };

            mockModel.Setup(x => x.GetPack(item.Id)).Returns(item);
            mockModel.Setup(x => x.DeletePack(item.Id));
            mockModel.Setup(x => x.UpdatePack(item)).Throws(new Exception("In this case, the UpdatePack should not be called."));

            // Act
            service.RemoveItemFromPack(item.Id);
        }
        */
    }
}
