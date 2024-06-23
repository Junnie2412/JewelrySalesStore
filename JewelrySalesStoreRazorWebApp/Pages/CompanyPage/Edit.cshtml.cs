using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.CompanyPage
{
    public class EditModel : PageModel
    {
        private readonly CompanyBusiness _business;

        public EditModel()
        {
            _business ??= new CompanyBusiness();
        }

        [BindProperty]
        public Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _business.GetById(id);

            if (result.Data == null)
            {
                return NotFound();
            }

            Company = result.Data as Company;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _business.Update(Company);

            return RedirectToPage("./Index");
        }
    }
}
