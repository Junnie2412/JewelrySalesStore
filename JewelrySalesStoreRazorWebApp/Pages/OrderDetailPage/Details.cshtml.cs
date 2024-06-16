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
    public class DetailsModel : PageModel
    {
        private readonly IOrderDetailBusiness _business;

        public DetailsModel()
        {
            _business ??= new OrderDetailBusiness();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _business.GetById(id);
            if (result.Status > 0 && result.Data != null)
            {
                OrderDetail = result.Data as OrderDetail;
            }
            else
            {
                return NotFound();
            }

            return Page();
        }
    }
}
