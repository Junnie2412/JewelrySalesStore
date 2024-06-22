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
    public class DetailsModel : PageModel
    {
        private readonly IPromotionBusiness business;

        public DetailsModel()
        {
            business ??= new PromotionBusiness();
        }

        public Promotion Promotion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await business.GetById(id);
            if (promotion == null)
            {
                return NotFound();
            }
            else
            {
                Promotion = promotion.Data as Promotion;
            }
            return Page();
        }
    }
}
