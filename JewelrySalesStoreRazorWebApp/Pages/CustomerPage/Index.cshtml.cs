using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using Microsoft.AspNetCore.Http;

namespace JewelrySalesStoreRazorWebApp.Pages.CustomerPage
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchPhone { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CustomerVipStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CustomerNonVipStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 5;     //Số object trên một trang

        private readonly ICustomerBusiness business;
      

        public IndexModel()
        {
            business ??= new CustomerBusiness();
        }

        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                var customers = result.Data as List<Customer>;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    customers = customers.Where(c =>
                        (c.CustomerLastName != null && c.CustomerLastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase)) ||
                        (c.CustomerFirstName != null && c.CustomerFirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                if (!string.IsNullOrEmpty(SearchPhone))
                {
                    customers = customers.Where(c =>
                        c.CustomerPhone != null && c.CustomerPhone.Contains(SearchPhone)
                    ).ToList();
                }

                if (CustomerVipStatus)
                {
                    customers = customers.Where(c => c.CustomerVipStatus).ToList();
                }

                if (CustomerNonVipStatus)
                {
                    customers = customers.Where(c => !c.CustomerVipStatus).ToList();
                }

                // Phân trang
                TotalPages = (int)Math.Ceiling(customers.Count / (double)PageSize);
                Customer = customers.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();

            }

        }
    }
}
