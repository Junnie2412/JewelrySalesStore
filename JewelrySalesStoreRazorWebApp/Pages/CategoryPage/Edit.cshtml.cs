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

namespace JewelrySalesStoreRazorWebApp.Pages.CategoryPage
{
    public class EditModel : PageModel
    {
        private readonly CategoryBusiness _business;

        public EditModel()
        {
            _business ??= new CategoryBusiness();
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

            Category = category.Data as Category;
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
                await _business.Update(Category);
            }
            catch (Exception e)
            {
            }

            return RedirectToPage("./Index");
        }

        //private bool CategoryExists(Guid id)
        //{
        //    return _context.Categories.Any(e => e.CategoryId == id);
        //}
    }
}
