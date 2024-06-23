using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        [BindProperty]
        public Company Company { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _business.Save(Company);

            return RedirectToPage("./Index");
        }
    }
}
