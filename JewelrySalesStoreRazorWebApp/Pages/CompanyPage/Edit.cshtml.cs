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

            Company = company.Data as Company;
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
                await _business.Update(Company);
            }
            catch (Exception e)
            {
            }

            return RedirectToPage("./Index");
        }

        //private bool CompanyExists(Guid id)
        //{
        //    return _context.Companies.Any(e => e.CompanyId == id);
        //}
    }
}
