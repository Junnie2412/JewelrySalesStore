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
    public class IndexModel : PageModel
    {
        private readonly ICategoryBusiness _business;

        public IndexModel()
        {
            _business ??= new CategoryBusiness();
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var resultl = await _business.GetAll();
            if(resultl != null && resultl.Status > 0 && resultl.Data != null)
            {
                Category = resultl.Data as List<Category>;
            }
        }
    }
}
