using PARTS.DAL.Entities.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARTS.DAL.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid Сustomer { get; set; }
        public IEnumerable<Part> Parts { get; set; }

    }
}
