using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.CategoryPage
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryBusiness _business;

        public DetailsModel()
        {
            _business = new CategoryBusiness();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

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
                Category = category.Data as Category;
            }

            return Page();
        }
    }
}
