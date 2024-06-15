﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.CustomerPage
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CustomerVipStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CustomerNonVipStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 4;     //Số object trên một trang

        private readonly ICustomerBusiness business;
        //private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;
        //public IndexModel(JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext context)
        //{
        //    _context = context;
        //}

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