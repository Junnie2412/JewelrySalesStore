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
    public class DeleteModel : PageModel
    {
        private readonly IPromotionBusiness business;

        public DeleteModel()
        {
            business ??= new PromotionBusiness();
        }

        [BindProperty]
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
            Promotion = promotion.Data as Promotion;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await business.DeleteById(id);
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return RedirectToPage("./Index");
        }
    }
}
