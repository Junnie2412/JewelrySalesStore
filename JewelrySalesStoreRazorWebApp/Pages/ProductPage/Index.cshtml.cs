using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;

namespace JewelrySalesStoreRazorWebApp.Pages.ProductPage
{
    public class IndexModel : PageModel
    {
        private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;

        public IndexModel(JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Promotion).ToListAsync();
        }
    }
}
