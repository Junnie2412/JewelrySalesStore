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

        public IList<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>()!;

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public int CurrentPage { get; set; } = 5;
        public int PageSize { get; set; } = 7;
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, int? page)
        {
            CurrentSort = sortOrder;
            CurrentPage = page ?? 1;

            if (sortOrder == null)
            {
                sortOrder = CurrentSort;
            }

            ViewData["CurrentSort"] = sortOrder;

            if (!string.IsNullOrWhiteSpace(currentFilter))
            {
                CurrentFilter = currentFilter.Trim();
            }

            ViewData["CurrentFilter"] = CurrentFilter;

            var result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null)
            {
                var details = result.Data as List<OrderDetail>;

                if (!string.IsNullOrEmpty(CurrentFilter))
                {
                    details = details.Where(c =>
                        (c.OrderId.HasValue && c.OrderId.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        (c.ProductId.HasValue && c.ProductId.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        (c.Quantity.HasValue && c.Quantity.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        (c.UnitPrice.HasValue && c.UnitPrice.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        (c.TotalPrice.HasValue && c.TotalPrice.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        (c.DiscountPrice.HasValue && c.DiscountPrice.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        (c.FinalPrice.HasValue && c.FinalPrice.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        (c.IsActive.HasValue && c.IsActive.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(c.Notes) && c.Notes.Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase))
                    ).ToList();

                }

                TotalRecords = details.Count;
                TotalPages = (int)Math.Ceiling(TotalRecords / (double)PageSize);

                switch (sortOrder)
                {
                    case "Order ID":
                        details = details.OrderBy(c => c.OrderId).ToList();
                        break;
                    case "Product ID":
                        details = details.OrderBy(c => c.ProductId).ToList();
                        break;
                    case "Quantity":
                        details = details.OrderBy(c => c.Quantity).ToList();
                        break;
                    case "Unit Price":
                        details = details.OrderBy(c => c.UnitPrice).ToList();
                        break;
                    case "Total Price":
                        details = details.OrderBy(c => c.TotalPrice).ToList();
                        break;
                    case "Discount Price":
                        details = details.OrderBy(c => c.DiscountPrice).ToList();
                        break;
                    case "Final Price":
                        details = details.OrderBy(c => c.FinalPrice).ToList();
                        break;
                    case "IsActive":
                        details = details.OrderBy(c => c.IsActive).ToList();
                        break;
                    case "Notes":
                        details = details.OrderBy(c => c.Notes).ToList();
                        break;
                    default:
                        break;
                }

                OrderDetail = details.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            }
        }
    }
}
