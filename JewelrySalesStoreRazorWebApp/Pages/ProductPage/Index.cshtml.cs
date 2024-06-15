﻿using System;
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
    public class IndexModel : PageModel
    {
        private readonly IProductBusiness _business;

        public IndexModel()
        {
            _business ??= new ProductBusiness();
        }

        public IList<Product> ProductList { get; set; } = default!;
        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var resultl = await _business.GetAll();
            if (resultl != null && resultl.Status > 0 && resultl.Data != null)
            {
                var newProductList = resultl.Data as List<Product>;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    newProductList = newProductList.Where(c => c.Name != null && c.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                ProductList = newProductList;
            }
        }

        public async Task<IActionResult> OnGetImageAsync(Guid id)
        {
            var product = await _business.GetById(id);
            if (product != null && product.Status > 0 && product.Data != null)
            {
                Product = product.Data as Product;
            }

            return File(Product.Image, "image/jpeg");
        }
    }
}