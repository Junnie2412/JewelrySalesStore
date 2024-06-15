using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class DetailsModel : PageModel
    {
        private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;

        public DetailsModel(JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext context)
        {
            _context = context;
        }

        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                Order = order;
            }
            return Page();
        }
    }
}
