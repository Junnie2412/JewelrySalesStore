using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class CreateModel : PageModel
    {
        //private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;
        private readonly OrderBusiness business;

        public CreateModel()
        {
           business ??= new OrderBusiness();
        }

        public IActionResult OnGet()
        {
        //ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId");
        //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerAddress");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

          await business.Save(Order);

            return RedirectToPage("./Index");
        }
    }
}
