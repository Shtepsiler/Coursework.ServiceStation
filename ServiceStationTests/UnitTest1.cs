using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using PARTS.DAL.Data;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Repositories;
using Xunit.Sdk;

namespace ServiceStationTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            var testObject = new Brand();

            var context = new Mock<PartsDBContext>();
            var dbSetMock = new Mock<DbSet<Brand>>();
            context.Setup(x => x.Set<Brand>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<Brand>())).Returns((Brand entity) =>
            {
                // Mock EntityEntry for adding the entity
                var entry = new Mock<EntityEntry<Brand>>();
                entry.Setup(e => e.Entity).Returns(entity);
                return entry.Object;
            });

            // Act
            var repository = new BrandRepository(context.Object);
            await repository.InsertAsync(testObject);

            // Assert
            context.Verify(x => x.Set<Brand>(), Times.Once);
            dbSetMock.Verify(x => x.AddAsync(It.Is<Brand>(y => y == testObject),default), Times.Once);

        }

    }
}