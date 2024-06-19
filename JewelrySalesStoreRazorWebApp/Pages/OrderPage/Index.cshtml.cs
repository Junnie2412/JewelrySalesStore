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
        public string? SearchCustomerAddress { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchShippingMethod { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsInactive { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 5;

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
                if (!string.IsNullOrEmpty(SearchCustomerAddress))
                {
                    newOrders = newOrders.Where(c => c.CustomerAddress != null && c.CustomerAddress.Contains(SearchCustomerAddress, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(SearchShippingMethod))
                {
                    newOrders = newOrders.Where(c =>
                        (c.ShippingMethod != null && c.ShippingMethod.Contains(SearchShippingMethod, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }
                if (IsActive && !IsInactive)
                {
                    newOrders = newOrders.Where(c => c.Status).ToList();
                }
                else if (!IsActive && IsInactive)
                {
                    newOrders = newOrders.Where(c => !c.Status).ToList();
                }

                TotalPages = (int)Math.Ceiling(newOrders.Count / (double)PageSize);
                Order = newOrders.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

                //Order = newOrders;
            }
        }
    }
}
