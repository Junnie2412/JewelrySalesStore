using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using JewelrySalesStoreBusiness.BusinessOrder;
using Microsoft.EntityFrameworkCore;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderDetailPage
{
    public class CreateModel : PageModel
    {
        private readonly OrderDetailBusiness _business;
        private readonly OrderBusiness _order;
        private readonly ProductBusiness _product;

        public CreateModel()
        {
            _business ??= new OrderDetailBusiness();
            _order ??= new OrderBusiness();
            _product ??= new ProductBusiness();
        }

        public SelectList ProductsSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateProductsSelectListAsync();
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = new OrderDetail();

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateProductsSelectListAsync();
                return Page();
            }

            try
            {
                var productResult = await _product.GetById(OrderDetail.ProductId.Value);
                if (productResult.Status > 0 && productResult.Data != null)
                {
                    var product = (Product)productResult.Data;
                    OrderDetail.UnitPrice = product.Price ?? 0;
                    OrderDetail.TotalPrice = OrderDetail.Quantity * OrderDetail.UnitPrice;
                    OrderDetail.FinalPrice = OrderDetail.TotalPrice - (OrderDetail.DiscountPrice ?? 0);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Product not found.");
                    await PopulateProductsSelectListAsync();
                    return Page();
                }

                var saveResult = await _business.Save(OrderDetail);
                if (saveResult.Status <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Failed to save OrderDetail.");
                    await PopulateProductsSelectListAsync();
                    return Page();
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                await PopulateProductsSelectListAsync();
                return Page();
            }
        }

        private async Task PopulateProductsSelectListAsync()
        {
            var productsResult = await _product.GetAll();
            if (productsResult.Status > 0 && productsResult.Data != null)
            {
                var productList = (IEnumerable<Product>)productsResult.Data;

                ProductsSelectList = new SelectList(productList, "ProductId", "Name");
            }
            else
            {
                ProductsSelectList = new SelectList(new List<Product>(), "ProductId", "Name");
            }
        }
    }
}
