using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class CreateModel : PageModel
    {
        private readonly OrderBusiness _business;
        private readonly ProductBusiness _product;
        private readonly CompanyBusiness _company;
        private readonly CustomerBusiness _customer;
        private readonly OrderDetailBusiness _detail;
        private readonly PromotionBusiness _promotion;

        public CreateModel()
        {
            _business ??= new OrderBusiness();
            _product ??= new ProductBusiness();
            _company ??= new CompanyBusiness();
            _customer ??= new CustomerBusiness();
            _detail ??= new OrderDetailBusiness();
            _promotion ??= new PromotionBusiness();
        }

        public SelectList ProductsSelectList { get; set; }
        public SelectList PromotionsSelectList { get; set; }
        public SelectList CompaniesSelectList { get; set; }
        public SelectList CustomersNameSelectList { get; set; }
        public SelectList CustomersAddressSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateProductsSelectListAsync();
            await PopulatePromotionsSelectListAsync();
            await PopulateCompaniesSelectListAsync();
            await PopulateCustomersNameSelectListAsync();
            await PopulateCustomersAddressSelectListAsync();
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = new Order();

        [BindProperty]
        public Product Product { get; set; } = new Product();

        [BindProperty]
        public Guid ProductId { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public Guid PromotionId { get; set; }

        [BindProperty]
        public string PromotionCode { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
     
            if (!ModelState.IsValid)
            {
                await PopulateDropdownListsAsync();
                return Page();
            }

            try
            {
                var productResult = await _product.GetById(ProductId);
                var product = productResult.Data as Product;
                if (product == null)
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve Product details.");
                    await PopulateDropdownListsAsync();
                    return Page();
                }

                var customerResult = await _customer.GetById((Guid)Order.CustomerId);
                var customer = customerResult.Data as Customer;
                if (customer == null)
                {
                    ModelState.AddModelError(string.Empty, "Failed to retrieve Customer details.");
                    await PopulateDropdownListsAsync();
                    return Page();
                }

                double discountPrice = 0.0;
                if (PromotionId != Guid.Empty)
                {
                    var promotionResult = await _promotion.GetById(PromotionId);
                    var promotion = promotionResult.Data as Promotion;
                    if (promotion == null || !promotion.IsActive == true || !promotion.PromotionCode.Equals(PromotionCode, StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError("PromotionCode", "Invalid or inactive promotion code.");
                        await PopulateDropdownListsAsync();
                        return Page();
                    }
                    discountPrice = (double)((product.Price * Quantity) * (promotion.DiscountPercentage / 100));
                }

                var finalPrice = (Quantity * product.Price) - discountPrice;

                Order.TotalPrice = finalPrice;
                Order.Date = DateTime.Today;
                Order.Status = true;
                Order.CustomerAddress = customer.CustomerAddress;

                var saveResult = await _business.Save(Order);
                if (saveResult.Status <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Failed to save Order.");
                    await PopulateDropdownListsAsync();
                    return Page();
                }

                var orderDetail = new OrderDetail
                {
                    OrderId = Order.OrderId,
                    ProductId = ProductId,
                    Quantity = Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = Quantity * product.Price,
                    DiscountPrice = discountPrice,
                    FinalPrice = finalPrice,
                    IsActive = Order.Status,
                };

                var orderDetailSaveResult = await _detail.Save(orderDetail);
                if (orderDetailSaveResult.Status <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Failed to save OrderDetail.");
                    await PopulateDropdownListsAsync();
                    return Page();
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                await PopulateDropdownListsAsync();
                return Page();
            }
        }

        private async Task PopulateDropdownListsAsync()
        {
            await PopulateProductsSelectListAsync();
            await PopulatePromotionsSelectListAsync();
            await PopulateCompaniesSelectListAsync();
            await PopulateCustomersNameSelectListAsync();
            await PopulateCustomersAddressSelectListAsync();
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
            ViewData["ProductsSelectList"] = ProductsSelectList;
        }

        private async Task PopulatePromotionsSelectListAsync()
        {
            var promotionsResult = await _promotion.GetAll();
            if (promotionsResult.Status > 0 && promotionsResult.Data != null)
            {
                var promotionList = (IEnumerable<Promotion>)promotionsResult.Data;
                PromotionsSelectList = new SelectList(promotionList, "PromotionId", "PromotionCode");
            }
            else
            {
                PromotionsSelectList = new SelectList(new List<Promotion>(), "PromotionId", "PromotionCode");
            }
            ViewData["PromotionsSelectList"] = PromotionsSelectList;
        }

        private async Task PopulateCompaniesSelectListAsync()
        {
            var companiesResult = await _company.GetAll();
            if (companiesResult.Status > 0 && companiesResult.Data != null)
            {
                var companyList = (IEnumerable<Company>)companiesResult.Data;
                CompaniesSelectList = new SelectList(companyList, "CompanyId", "CompanyName");
            }
            else
            {
                CompaniesSelectList = new SelectList(new List<Company>(), "CompanyId", "CompanyName");
            }
            ViewData["CompaniesSelectList"] = CompaniesSelectList;
        }

        private async Task PopulateCustomersNameSelectListAsync()
        {
            var customersResult = await _customer.GetAll();
            if (customersResult.Status > 0 && customersResult.Data != null)
            {
                var customerList = (IEnumerable<Customer>)customersResult.Data;
                CustomersNameSelectList = new SelectList(customerList, "CustomerId", "CustomerFirstName");
            }
            else
            {
                CustomersNameSelectList = new SelectList(new List<Customer>(), "CustomerId", "CustomerFirstName");
            }
            ViewData["CustomersNameSelectList"] = CustomersNameSelectList;
        }

        private async Task PopulateCustomersAddressSelectListAsync()
        {
            var customersResult = await _customer.GetAll();
            if (customersResult.Status > 0 && customersResult.Data != null)
            {
                var customerList = (IEnumerable<Customer>)customersResult.Data;
                CustomersAddressSelectList = new SelectList(customerList, "CustomerId", "CustomerAddress");
            }
            else
            {
                CustomersAddressSelectList = new SelectList(new List<Customer>(), "CustomerId", "CustomerAddress");
            }
            ViewData["CustomersAddressSelectList"] = CustomersAddressSelectList;
        }

        public async Task<IActionResult> OnGetCustomerAddressAsync(Guid customerId)
        {
            var customerResult = await _customer.GetById(customerId);
            if (customerResult.Status > 0 && customerResult.Data != null)
            {
                var customer = customerResult.Data as Customer;
                return new JsonResult(new { customerAddress = customer.CustomerAddress });
            }
            return new JsonResult(new { customerAddress = string.Empty });
        }

        public async Task<IActionResult> OnGetCalculateTotalPriceAsync(Guid productId, int quantity, Guid? promotionId)
        {
            var productResult = await _product.GetById(productId);
            var product = productResult.Data as Product;
            if (product == null)
            {
                return new JsonResult(new { totalPrice = 0.0 });
            }

            double discountPrice = 0.0;
            if (promotionId.HasValue)
            {
                var promotionResult = await _promotion.GetById(promotionId.Value);
                var promotion = promotionResult.Data as Promotion;
                if (promotion != null && promotion.IsActive == true)
                {
                    discountPrice = (double)((product.Price * quantity) * (promotion.DiscountPercentage / 100));
                }
            }

            var finalPrice = (quantity * product.Price) - discountPrice;
            return new JsonResult(new { totalPrice = finalPrice });
        }
    }
}
