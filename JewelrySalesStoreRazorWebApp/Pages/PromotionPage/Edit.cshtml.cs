using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.PromotionPage
{
    public class EditModel : PageModel
    {
        private readonly IPromotionBusiness business;

        public EditModel()
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

            var promotion =  await business.GetById(id);
            if (promotion == null)
            {
                return NotFound();
            }
            Promotion = promotion.Data as Promotion;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           

            try
            {
                await business.Update(Promotion);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            
            return RedirectToPage("./Index");
        }

        //private bool PromotionExists(Guid id)
        //{
        //    return _context.Promotions.Any(e => e.PromotionId == id);
        //}
    }
}
