using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusines;

namespace JewelrySalesStoreRazorWebApp.Pages.PromotionPage
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Promotionisative { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 4;

        private readonly IPromotionBusiness business;


        public IndexModel()
        {
            business ??= new PromotionBusiness();
          
        }

        public IList<Promotion> Promotion { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                var promotion = result.Data as List<Promotion>;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    promotion = promotion.Where(c =>
                        (c.PromotionName != null && c.PromotionName.Contains(SearchString, StringComparison.OrdinalIgnoreCase)) 
                    ).ToList();
                }

                if (Promotionisative )
                {
                    promotion = promotion.Where(c => (bool)c.IsActive).ToList();
                }


                // Phân trang
                TotalPages = (int)Math.Ceiling(promotion.Count / (double)PageSize);
                Promotion = promotion.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            }

        }
    }
}