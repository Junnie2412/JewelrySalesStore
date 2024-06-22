using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.ProductPage
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        private readonly int PageSize = 5;     // Số object trên một trang

        private readonly IProductBusiness _business;

        public IndexModel()
        {
            _business ??= new ProductBusiness();
        }

        public IList<Product> ProductList { get; set; } = new List<Product>();
        [BindProperty]
        public Product Product { get; set; } = new Product();
        [BindProperty(SupportsGet = true)]
        public string? SearchName { get; set; }
        [BindProperty(SupportsGet = true)]
        public float? PriceFrom { get; set; }
        [BindProperty(SupportsGet = true)]
        public float? PriceTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsNotActive { get; set; }

        public async Task OnGetAsync()
        {
            var result = await _business.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                var newProductList = result.Data as List<Product>;
                if (!string.IsNullOrEmpty(SearchName))
                {
                    newProductList = newProductList
                        .Where(c => c.Name != null && c.Name.Contains(SearchName, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                if (PriceFrom != null && PriceTo != null)
                {
                    newProductList = newProductList
                        .Where(c => c.Price != null && c.Price >= PriceFrom && c.Price <= PriceTo)
                        .ToList();
                }
                if (IsActive)
                {
                    newProductList = newProductList.Where(c => (bool)c.IsActive).ToList();
                }

                if (IsNotActive)
                {
                    newProductList = newProductList.Where(c => !(bool)c.IsActive).ToList();
                }

                ProductList = newProductList;

                // Phân trang
                TotalPages = (int)Math.Ceiling(ProductList.Count / (double)PageSize);
                ProductList = newProductList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
        }

        public async Task<IActionResult> OnGetImageAsync(Guid id)
        {
            var product = await _business.GetById(id);
            if (product != null && product.Status > 0 && product.Data != null)
            {
                Product = product.Data as Product;
            }

            if (Product.Image != null && Product.Image.Length > 0)
            {
                return File(Product.Image, "image/jpeg");
            }
            else
            {
                return NotFound(); 
            }
        }
    }
}
