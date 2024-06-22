
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.PromotionPage
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public float? DiscountFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public float?DiscountTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsInactive { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 5;

        private readonly IPromotionBusiness business;


        public IndexModel()
        {
            business ??= new PromotionBusiness();

        }

        public IList<Promotion> Promotion { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                var promotion = result.Data as List<Promotion>;
                if (!string.IsNullOrEmpty(SearchName))
                {
                    promotion = promotion.Where(c =>
                        (c.PromotionName != null && c.PromotionName.Contains(SearchName, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                if (DiscountFrom != null && DiscountTo != null)
                {
                    promotion = promotion
                        .Where(c => c.DiscountPercentage != null 
                        && c.DiscountPercentage >= DiscountFrom 
                        && c.DiscountPercentage <= DiscountTo)
         
                        .ToList();
                }

                if (IsActive && !IsInactive)
                {
                    promotion = promotion.Where(c => (bool)c.IsActive).ToList();
                }
                else if (!IsActive && IsInactive)
                {
                    promotion = promotion.Where(c => (bool)!c.IsActive).ToList();
                }

                // Phân trang
                TotalPages = (int)Math.Ceiling(promotion.Count / (double)PageSize);
                Promotion = promotion.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
        }
    }
}
