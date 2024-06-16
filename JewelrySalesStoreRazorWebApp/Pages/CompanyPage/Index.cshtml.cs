using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.CompanyPage
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsInactive { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 4;

        private readonly ICompanyBusiness _business;
        //private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;
        //public IndexModel(JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext context)
        //{
        //    _context = context;
        //}

        public IndexModel()
        {
            _business ??= new CompanyBusiness();
        }

        public IList<Company> Company { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                var companies = result.Data as List<Company>;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    companies = companies.Where(c =>
                        (c.CompanyName != null && c.CompanyName.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                if (IsActive)
                {
                    companies = companies.Where(c => c.IsActive).ToList();
                }

                if (IsInactive)
                {
                    companies = companies.Where(c => !c.IsActive).ToList();
                }

                // Phân trang
                TotalPages = (int)Math.Ceiling(companies.Count / (double)PageSize);
                Company = companies.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            }
   
        }
    }
}
