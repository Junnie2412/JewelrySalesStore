using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusines;

namespace JewelrySalesStoreRazorWebApp.Pages.PromotionPage
{
    public class CreateModel : PageModel
    {
        private readonly IPromotionBusiness business;
        
        public CreateModel()
        {
           business ??= new PromotionBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Promotion Promotion { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

          
            await business.Save(Promotion);

            return RedirectToPage("./Index");
        }
    }
}
