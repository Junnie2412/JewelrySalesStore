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

namespace JewelrySalesStoreRazorWebApp.Pages.OrderDetailPage
{
    public class EditModel : PageModel
    {
        private readonly OrderDetailBusiness _business;

        public EditModel()
        {
            _business ??= new OrderDetailBusiness();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _business.GetById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            OrderDetail = orderDetail.Data as OrderDetail;
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
                await _business.Update(OrderDetail);
            }
            catch (Exception e)
            {
            }

            return RedirectToPage("./Index");
        }

        //private bool OrderDetailExists(Guid id)
        //{
        //    return _context.OrderDetails.Any(e => e.OrderDetailId == id);
        //}
    }
}
