using Microsoft.EntityFrameworkCore;
using Moq;
using PARTS.DAL.Data;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Repositories;
using ServiceStationTests.Mocks.PartsDAl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStationTests.Tests.BLLServicesTests
{
    public class BrahdServiceTest
    {
        Brand testObject = new Brand()
        {
            Id = Guid.Parse("507eedf8-8343-4bff-98ac-d959bc7fa578"),
            Title = "Test9",
            Description = "Test9",
            Timestamp = DateTime.Now

        };

        MockBrandRepositogy moq;
        BrandRepository repository;

        public BrahdServiceTest()
        {
             moq = new MockBrandRepositogy();
            repository = moq.GetBrandRepository();
        }

        [Fact]
        public async Task Test_InsertAsync()
        {
            // Arrange


            // Act

            await repository.InsertAsync(testObject);

            // Assert
            moq._contextMock.Verify(x => x.Set<Brand>(), Times.Once);
            moq._dbSetMock.Verify(x => x.AddAsync(It.Is<Brand>(y => y == testObject), default), Times.Once);
            moq._contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);




        }
        [Fact]
        public async Task Test_GetByIdAsync()
        {
            // Arrange

          

            // Act


           var res = await repository.GetByIdAsync(testObject.Id);

            // Assert
            moq._contextMock.Verify(x => x.Set<Brand>(), Times.Once);
            moq._dbSetMock.Verify(x => x.FindAsync(testObject.Id), Times.Once);




        }

    }
}