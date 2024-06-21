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
        [BindProperty(SupportsGet = true)]
        public int? MinQuantity { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MaxQuantity { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinFinalPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxFinalPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsInactive { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 5;

        private readonly IOrderDetailBusiness _business;

        public IndexModel()
        {
            _business ??= new OrderDetailBusiness();
        }

        public IList<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>()!;

        public async Task OnGetAsync()
        {
            var result = await _business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                var orderDetails = result.Data as List<OrderDetail>;

                // Filter by Quantity Range
                if (MinQuantity.HasValue && MaxQuantity.HasValue)
                {
                    orderDetails = orderDetails.Where(od =>
                        (od.Quantity >= MinQuantity && od.Quantity <= MaxQuantity)
                    ).ToList();
                }

                // Filter by FinalPrice Range
                if (MinFinalPrice.HasValue && MaxFinalPrice.HasValue)
                {
                    orderDetails = orderDetails.Where(od =>
                        (od.FinalPrice >= (double)MinFinalPrice && od.FinalPrice <= (double)MaxFinalPrice)
                    ).ToList();
                }

                // Filter by Active/Inactive
                if (IsActive)
                {
                    orderDetails = orderDetails.Where(c => c.IsActive).ToList();
                }

                if (IsInactive)
                {
                    orderDetails = orderDetails.Where(c => !c.IsActive).ToList();
                }

                // Pagination
                TotalPages = (int)Math.Ceiling(orderDetails.Count / (double)PageSize);
                OrderDetail = orderDetails.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
        }
    }
}
