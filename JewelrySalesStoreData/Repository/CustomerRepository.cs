using JewelrySalesStoreData.Base;
using JewelrySalesStoreData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreData.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository()
        {

        }

        public CustomerRepository(Net1702_221_4_JewelrySalesStoreContext context) : base(context) => _context = context;

    }
}
