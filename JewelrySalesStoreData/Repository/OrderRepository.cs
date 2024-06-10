using JewelrySalesStoreData.Base;
using JewelrySalesStoreData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreData.Repository
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository() { }
        public OrderRepository(Net1702_221_4_JewelrySalesStoreContext context) : base(context) => _context = context;
    }
}
