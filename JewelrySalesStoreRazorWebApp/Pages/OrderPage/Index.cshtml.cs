using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class IndexModel : PageModel
    {
        //private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;
        private readonly IOrderBusiness _business;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IndexModel()
        {
            _business ??= new OrderBusiness();
        }

        public IList<Order> Order { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var resultl = await _business.GetAll();
            if (resultl != null && resultl.Status > 0 && resultl.Data != null)
            {
                var newOrders = resultl.Data as List<Order>;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    newOrders = newOrders.Where(c => c.CustomerAddress != null && c.CustomerAddress.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                Order = newOrders;
            }
        }
    }
}
