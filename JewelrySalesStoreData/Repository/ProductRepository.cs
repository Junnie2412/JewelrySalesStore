using JewelrySalesStoreData.Base;
using JewelrySalesStoreData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreData.Repository
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(Net1702_221_4_JewelrySalesStoreContext context) => _context = context;
    }
}
