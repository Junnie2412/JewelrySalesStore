using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;

namespace JewelrySalesStoreRazorWebApp.Pages.CompanyPage
{
    public class DetailsModel : PageModel
    {
        private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;

        public DetailsModel(JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext context)
        {
            _context = context;
        }

        public Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }
            else
            {
                Company = company;
            }
            return Page();
        }
    }
}
