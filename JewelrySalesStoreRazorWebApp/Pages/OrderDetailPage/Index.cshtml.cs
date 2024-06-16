using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderDetailPage
{
    public class IndexModel : PageModel
    {

        private readonly IOrderDetailBusiness _business;

        public IndexModel()
        {
            _business ??= new OrderDetailBusiness();
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsInactive { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 4;

        public IList<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>()!;

        public async Task OnGetAsync()
        {
            var result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null)
            {
                var orderDetails = result.Data as List<OrderDetail>;
                //if (!string.IsNullOrEmpty(SearchString))
                //{
                //    orderDetails = orderDetails.Where(o =>
                //        (o.OrderId != null && o.OrderId.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                //    ).ToList();
                //}
                if (!string.IsNullOrEmpty(SearchString))
                {
                    orderDetails = orderDetails.Where(o =>
                        (o.Product.Name != null && o.Product.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                if (IsActive)
                {
                    orderDetails = orderDetails.Where(c => c.IsActive).ToList();
                }

                if (IsInactive)
                {
                    orderDetails = orderDetails.Where(c => !c.IsActive).ToList();
                }

                // Phân trang
                TotalPages = (int)Math.Ceiling(orderDetails.Count / (double)PageSize);
                OrderDetail = orderDetails.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            }

        }
    }
}
