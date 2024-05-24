using PARTS.DAL.Data;
using PARTS.DAL.Entities;
using PARTS.DAL.Excepstions;
using PARTS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.DAL.Repositories
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(PartsDBContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task AddPartToOrderAsync(Guid orderId, Guid partId)
        {
        

                var order = databaseContext.Orders.Find(orderId);
                if (order == null) throw new EntityNotFoundException($"order {orderId} not found");

                var part = databaseContext.Parts.Find(partId);
                if (part == null) throw new EntityNotFoundException($"part {partId} not found");

                order.Parts.Add(part);
                await databaseContext.SaveChangesAsync();
         

        }

        public async Task RemovePartFromOrderAsync(Guid orderId, Guid partId)
        {

            var order = databaseContext.Orders.Find(orderId);
            if (order == null) throw new EntityNotFoundException($"order {orderId} not found");

            var part = databaseContext.Parts.Find(partId);
            if (part == null) throw new EntityNotFoundException($"part {partId} not found");

            order.Parts.Remove(part);
            await databaseContext.SaveChangesAsync();
        }
    }
}
