using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class EditModel : PageModel
    {
        //private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;
        private readonly OrderBusiness business;

        public EditModel()
        {
            business ??= new OrderBusiness();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await business.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            //Order = order;
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId");
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerAddress");
            Order = order.Data as Order;
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

            //_context.Attach(Order).State = EntityState.Modified;

            try
            {
                await business.Update(Order);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!OrderExists(Order.OrderId))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                throw;
            }

            return RedirectToPage("./Index");
        }

        //private bool OrderExists(Guid id)
        //{
        //    return _context.Orders.Any(e => e.OrderId == id);
        //}
    }
}
