using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JewelrySalesStoreRazorWebApp.Pages.CategoryPage
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryBusiness _business;

        [BindProperty(SupportsGet = true)]
        public string? SearchName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchPurpose { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsNotActive { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        private readonly int PageSize = 5;     //Số object trên một trang

        public IndexModel()
        {
            _business ??= new CategoryBusiness();
        }

        public IList<Category> Category { get; set; } = default!;
        public List<SelectListItem> PurposeOptions { get; set; } = new List<SelectListItem>();

        public async Task OnGetAsync()
        {
            var resultl = await _business.GetAll();

            if (resultl != null && resultl.Status > 0 && resultl.Data != null)
            {
                var newCategories = resultl.Data as List<Category>;

                PurposeOptions = newCategories
                .Select(c => c.Purpose)
                .Distinct()
                .Where(p => !string.IsNullOrEmpty(p))
                .Select(p => new SelectListItem { Value = p, Text = p })
                .ToList();

                if (!string.IsNullOrEmpty(SearchName))
                {
                    newCategories = newCategories.Where(c => c.Name != null && c.Name.Contains(SearchName, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (!string.IsNullOrEmpty(SearchPurpose))
                {
                    newCategories = newCategories.Where(c => c.Purpose != null && c.Purpose.Contains(SearchPurpose)).ToList();
                }

                if (IsActive)
                {
                    newCategories = newCategories.Where(c => (bool)c.IsActive).ToList();
                }

                if (IsNotActive)
                {
                    newCategories = newCategories.Where(c => (bool)!c.IsActive).ToList();
                }

                Category = newCategories;
            }

            // Phân trang
            TotalPages = (int)Math.Ceiling(Category.Count / (double)PageSize);
            Category = Category.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
