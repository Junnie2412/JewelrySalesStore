using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.CompanyPage
{
    public class DeleteModel : PageModel
    {
        private readonly ICompanyBusiness _business;

        public DeleteModel()
        {
            _business ??= new CompanyBusiness();
        }

        [BindProperty]
        public Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _business.GetById(id);

            if (company == null)
            {
                return NotFound();
            }
            else
            {
                Company = company.Data as Company;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _business.DeleteById(id);

            return RedirectToPage("./Index");
        }
    }
}
