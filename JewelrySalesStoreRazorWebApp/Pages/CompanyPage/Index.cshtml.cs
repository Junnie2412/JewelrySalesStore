using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.CompanyPage
{
    public class IndexModel : PageModel
    {
        private readonly ICompanyBusiness _business;

        public IndexModel()
        {
            _business ??= new CompanyBusiness();
        }

        public IList<Company> Company { get; set; } = new List<Company>();

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public int CurrentPage { get; set; } = 3;
        public int PageSize { get; set; } = 7;
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, int? page)
        {
            CurrentSort = sortOrder;
            CurrentPage = page ?? 1;

            if (sortOrder == null)
            {
                sortOrder = CurrentSort;
            }

            ViewData["CurrentSort"] = sortOrder;

            if (!string.IsNullOrWhiteSpace(currentFilter))
            {
                CurrentFilter = currentFilter.Trim();
            }

            ViewData["CurrentFilter"] = CurrentFilter;

            var result = await _business.GetAll();

            if (result != null && result.Status > 0 && result.Data != null)
            {
                var companies = result.Data as List<Company>;

                if (!string.IsNullOrEmpty(CurrentFilter))
                {
                    companies = companies.Where(c =>
                        c.CompanyName.Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase) ||
                        c.CompanyAddress.Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase) ||
                        c.CompanyDescription.Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase) ||
                        (c.CompanyPhone != null && c.CompanyPhone.Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        c.Website.Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase) ||
                        (c.FoundationDate.HasValue && c.FoundationDate.Value.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        c.Email.Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase) ||
                        (c.IsActive != null && c.IsActive.ToString().Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)) ||
                        c.Notes.Contains(CurrentFilter, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }

                TotalRecords = companies.Count;
                TotalPages = (int)Math.Ceiling(TotalRecords / (double)PageSize);

                switch (sortOrder)
                {
                    case "Name":
                        companies = companies.OrderBy(c => c.CompanyName).ToList();
                        break;
                    case "Address":
                        companies = companies.OrderBy(c => c.CompanyAddress).ToList();
                        break;
                    case "Description":
                        companies = companies.OrderBy(c => c.CompanyDescription).ToList();
                        break;
                    case "Phone":
                        companies = companies.OrderBy(c => c.CompanyPhone).ToList();
                        break;
                    case "Website":
                        companies = companies.OrderBy(c => c.Website).ToList();
                        break;
                    case "Foundation":
                        companies = companies.OrderBy(c => c.FoundationDate).ToList();
                        break;
                    case "Email":
                        companies = companies.OrderBy(c => c.Email).ToList();
                        break;
                    case "IsActive":
                        companies = companies.OrderBy(c => c.IsActive).ToList();
                        break;
                    case "Notes":
                        companies = companies.OrderBy(c => c.Notes).ToList();
                        break;
                    default:
                        break;
                }

                Company = companies.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            }
        }
    }
}
