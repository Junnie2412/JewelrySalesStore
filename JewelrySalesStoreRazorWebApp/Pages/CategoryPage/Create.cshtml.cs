using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.CategoryPage
{
    public class CreateModel : PageModel
    {
        private readonly CategoryBusiness _business;

        public CreateModel()
        {
            _business ??= new CategoryBusiness();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _business.Save(Category);

            return RedirectToPage("./Index");
        }
    }
}
