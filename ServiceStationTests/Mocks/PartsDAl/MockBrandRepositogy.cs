using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using PARTS.BLL.Services.Interaces;
using PARTS.DAL.Data;
using PARTS.DAL.Entities.Item;
using PARTS.DAL.Interfaces;
using PARTS.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ServiceStationTests.Mocks.PartsDAl
{
    public class MockBrandRepositogy
    {
        public Mock<PartsDBContext> _contextMock;
        public BrandRepository _repository;
        public Mock<DbSet<Brand>> _dbSetMock;
        public List<Brand> mockList =  new();

        private Mock<DbSet<Brand>> CreateDbSetMock(Brand testObject)
        {
            Mock<DbSet<Brand>> mock = new Mock<DbSet<Brand>>();
            var dbSetMock = mock;
    
            dbSetMock.Setup(x => x.Add(It.IsAny<Brand>())).Returns((Brand entity) =>
            {
                // Mock EntityEntry for adding the entity
                var entry = new Mock<EntityEntry<Brand>>();
                entry.Setup(e => e.Entity).Returns(entity);
                mockList.Add(entity);
                return entry.Object;
            });
            dbSetMock.Setup(x => x.Update(It.IsAny<Brand>())).Returns((Brand entity) =>
            {
                // Mock EntityEntry for adding the entity
                var enttToremove = mockList.Find(x => x.Id == entity.Id);
                mockList.Remove(enttToremove);
                mockList.Add(entity); // Додати елемент у ваш список
                return null; // Повертаємо null, оскільки метод Update не повертає нічого
            });
            dbSetMock.Setup(x => x.Remove(It.IsAny<Brand>())).Returns((Brand entity) =>
            {
                // Mock EntityEntry for adding the entity
                var enttToremove = mockList.Find(x => x.Id == entity.Id);
                mockList.Remove(enttToremove); return null; // Повертаємо null, оскільки метод Remove не повертає нічого
            });
            dbSetMock.Setup(x => x.AddAsync(It.IsAny<Brand>(),It.IsAny<CancellationToken>())).Returns((Brand entity) =>
            {
                // Mock EntityEntry for adding the entity
                var entry = new Mock<EntityEntry<Brand>>();
                entry.Setup(e => e.Entity).Returns(entity);
                mockList.Add(entity);
                return entry.Object;
            });
            dbSetMock.Setup(x => x.FindAsync(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                // Mock EntityEntry for adding the entity
                var entry = new Mock<EntityEntry<Brand>>();
                entry.Setup(p=>p.Entity).Returns(mockList.Find(p=>p.Id == id));
                return entry.Object;
            });
            dbSetMock.Setup(x => x.ToListAsync(default)).Returns((List<Brand> entity) =>
            {

                return mockList;
            });

            return dbSetMock;
        }

        public MockBrandRepositogy()
        {
            _contextMock = new Mock<PartsDBContext>();
            var testObject = new Brand();
            _dbSetMock = CreateDbSetMock(testObject);
            _contextMock.Setup(x => x.Set<Brand>()).Returns(_dbSetMock.Object);
            _repository = new BrandRepository(_contextMock.Object);




        }

        public BrandRepository GetBrandRepository()
        {
            return _repository;
        }

    }
}