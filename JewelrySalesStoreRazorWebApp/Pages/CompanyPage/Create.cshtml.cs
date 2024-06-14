using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.CompanyPage
{
    public class CreateModel : PageModel
    {
        private readonly CompanyBusiness _business;

        public CreateModel()
        {
            _business ??= new CompanyBusiness();
        }

        public IActionResult OnGet()
        {
            Company = new Company();
            return Page();
        }

        [BindProperty]
        public Company Company { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Company.CompanyId = Guid.NewGuid();

            await _business.Save(Company);

            return RedirectToPage("./Index");
        }
    }
}
