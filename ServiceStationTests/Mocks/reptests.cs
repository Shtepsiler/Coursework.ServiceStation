using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using PARTS.DAL.Data;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Excepstions;
using PARTS.DAL.Repositories;
using Xunit;

namespace PARTS.DAL.Tests.Repositories
{
    public class BrandRepositoryTests
    {
        [Fact]
        public async Task GetByIdAsync_WithExistingId_ReturnsBrand()
        {
            // Arrange
            var brandId = Guid.NewGuid();
            var brandName = "Brand1";
            var mockBrands = new List<Brand>
            {
                new Brand { Id = brandId, Title = brandName },
                new Brand { Id = Guid.NewGuid(), Title = "Brand2" }
            };

            var mockSet = new Mock<DbSet<Brand>>();
            mockSet.As<IQueryable<Brand>>().Setup(m => m.Provider).Returns(mockBrands.AsQueryable().Provider);
            mockSet.As<IQueryable<Brand>>().Setup(m => m.Expression).Returns(mockBrands.AsQueryable().Expression);
            mockSet.As<IQueryable<Brand>>().Setup(m => m.ElementType).Returns(mockBrands.AsQueryable().ElementType);
            mockSet.As<IQueryable<Brand>>().Setup(m => m.GetEnumerator()).Returns(() => mockBrands.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.FindAsync(brandId)).ReturnsAsync(mockBrands.FirstOrDefault(b => b.Id == brandId));

            var mockContext = new Mock<PartsDBContext>();
            mockContext.Setup(c => c.Brands).Returns(mockSet.Object);

            var repository = new BrandRepository(mockContext.Object);

            // Act
            var brand = await repository.GetByIdAsync(brandId);

            // Assert
            Assert.NotNull(brand);
            Assert.Equal(brandName, brand.Title);
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistingId_ThrowsEntityNotFoundException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            var mockBrands = new List<Brand>();

            var mockSet = new Mock<DbSet<Brand>>();
            mockSet.As<IQueryable<Brand>>().Setup(m => m.Provider).Returns(mockBrands.AsQueryable().Provider);
            mockSet.As<IQueryable<Brand>>().Setup(m => m.Expression).Returns(mockBrands.AsQueryable().Expression);
            mockSet.As<IQueryable<Brand>>().Setup(m => m.ElementType).Returns(mockBrands.AsQueryable().ElementType);
            mockSet.As<IQueryable<Brand>>().Setup(m => m.GetEnumerator()).Returns(() => mockBrands.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.FindAsync(nonExistingId)).ReturnsAsync(mockBrands.FirstOrDefault(b => b.Id == nonExistingId));

            var mockContext = new Mock<PartsDBContext>();
            mockContext.Setup(c => c.Brands).Returns(mockSet.Object);

            var repository = new BrandRepository(mockContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => repository.GetByIdAsync(nonExistingId));
        }
    }
}
