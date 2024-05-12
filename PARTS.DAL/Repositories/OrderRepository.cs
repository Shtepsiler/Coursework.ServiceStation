using PARTS.DAL.Data;
using PARTS.DAL.Entities;
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






    }
}
