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

namespace JewelrySalesStoreRazorWebApp.Pages.CustomerPage
{
    public class EditModel : PageModel
    {
        private readonly ICustomerBusiness business;
        public EditModel()
        {
            business ??= new CustomerBusiness();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await business.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            Customer = customer.Data as Customer;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await business.Update(Customer);
            }
            catch (Exception ex)
            {

            }

            return RedirectToPage("./Index");
        }
    }
}
