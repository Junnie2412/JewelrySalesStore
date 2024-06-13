using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;

namespace JewelrySalesStoreRazorWebApp.Pages.ProductPage
{
    public class CreateModel : PageModel
    {
        private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;

        public CreateModel(JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
        ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionId");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
