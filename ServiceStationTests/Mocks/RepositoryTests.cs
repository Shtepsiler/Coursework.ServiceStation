using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using PARTS.DAL.Data;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationTests.Mocks
{
    public class RepositoryTests
    {
        private Mock<PartsDBContext> _contextMock;
        private Mock<DbSet<TEntity>> CreateDbSetMock<TEntity>(TEntity testObject) where TEntity : class
        {
            var dbSetMock = new Mock<DbSet<TEntity>>();
            dbSetMock.Setup(x => x.Add(It.IsAny<TEntity>())).Returns((TEntity entity) =>
            {
                // Mock EntityEntry for adding the entity
                var entry = new Mock<EntityEntry<TEntity>>();
                entry.Setup(e => e.Entity).Returns(entity);
                return entry.Object;
            });
            return dbSetMock;
        }

        public RepositoryTests()
        {
            _contextMock = new Mock<PartsDBContext>();
        }

        [Fact]
        public async Task TestInsertAsync()
        {
            // Arrange
            var testObject = new Brand();
            _contextMock.Setup(x => x.Set<Brand>()).Returns(CreateDbSetMock(testObject).Object);

            var repository = new BrandRepository(_contextMock.Object);

            // Act
            await repository.InsertAsync(testObject);

            // Assert
            _contextMock.Verify(x => x.Set<Brand>(), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);

        }
    }
}
