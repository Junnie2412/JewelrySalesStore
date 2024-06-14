using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.ProductPage
{
    public class DeleteModel : PageModel
    {
        private readonly IProductBusiness _business;

        public DeleteModel()
        {
            _business ??= new ProductBusiness();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _business.GetById(id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Product = category.Data as Product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _business.DeleteById(id);

            return RedirectToPage("./Index");
        }
    }
}
