using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderDetailPage
{
    public class DeleteModel : PageModel
    {
        private readonly IOrderDetailBusiness _business;

        public DeleteModel()
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
            else
            {
                OrderDetail = orderDetail.Data as OrderDetail;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _business.DeleteById(id);


            return RedirectToPage("./Index");
        }
    }
}
