using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class IndexModel : PageModel
    {
        //private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;
        private readonly IOrderBusiness business;

        public IndexModel()
        {
            //_context = context;
            business ??= new OrderBusiness();
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetAll();
                //.Include(o => o.Company)
                //.Include(o => o.Customer).ToListAsync();
                if(result!=null && result.Status > 0 && result.Data!=null)
            {
                Order = result.Data as List<Order>;
            }
        }
    }
}
