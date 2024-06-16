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
    public class DetailsModel : PageModel
    {
        private readonly ICompanyBusiness _business;

        public DetailsModel()
        {
            _business ??= new CompanyBusiness();
        }

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
    }
}
