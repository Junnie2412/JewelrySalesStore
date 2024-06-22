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
        public string? SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchPhone { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsInactive { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 5;

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

                // Filter by Name
                if (!string.IsNullOrEmpty(SearchName))
                {
                    companies = companies.Where(c =>
                        (c.CompanyName != null && c.CompanyName.Contains(SearchName, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                // Filter by Phone
                if (!string.IsNullOrEmpty(SearchPhone))
                {
                    companies = companies.Where(c =>
                        (c.CompanyPhone != null && c.CompanyPhone.Contains(SearchPhone, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                // Filter by Date Range
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    companies = companies.Where(c =>
                        (c.FoundationDate >= StartDate && c.FoundationDate <= EndDate)
                    ).ToList();
                }

                // Filter by Active/Inactive
                if (IsActive && !IsInactive)
                {
                    companies = companies.Where(c => c.IsActive).ToList();
                }
                else if (!IsActive && IsInactive)
                {
                    companies = companies.Where(c => !c.IsActive).ToList();
                }

                // Pagination
                TotalPages = (int)Math.Ceiling(companies.Count / (double)PageSize);
                Company = companies.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                Company = new List<Company>(); // Initialize an empty list if there is no data
            }
        }
    }
}
