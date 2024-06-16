using JewelrySalesStoreData.Base;
using JewelrySalesStoreData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesStoreData.Repository
{
    public class CompanyRepository : GenericRepository<Company>
    {
        public CompanyRepository()
        {
        }

        public CompanyRepository(Net1702_221_4_JewelrySalesStoreContext context) : base(context) => _context ??= context;
    }
}
