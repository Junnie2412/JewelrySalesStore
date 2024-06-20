using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;
using JewelrySalesStoreBusiness;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class EditModel : PageModel
    {
        //private readonly JewelrySalesStoreData.Models.Net1702_221_4_JewelrySalesStoreContext _context;
        private readonly OrderBusiness business;
        //private readonly ProductBusiness _product;
        //private readonly CompanyBusiness _company;
        //private readonly CustomerBusiness _customer;
        //private readonly OrderDetailBusiness _detail;

        public EditModel()
        {
            business ??= new OrderBusiness();
            //_product ??= new ProductBusiness();
            //_company ??= new CompanyBusiness();
            //_customer ??= new CustomerBusiness();
            //_detail ??= new OrderDetailBusiness();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        //[BindProperty]
        //public Product Product { get; set; } = default!;

        //[BindProperty]
        //public Guid ProductId { get; set; }

        //[BindProperty]
        //public int Quantity { get; set; }

        //public SelectList ProductsSelectList { get; set; }
        //public SelectList CompaniesSelectList { get; set; }
        //public SelectList CustomersNameSelectList { get; set; }
        //public SelectList CustomersAddressSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await business.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            //Order = order;
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId");
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerAddress");
            Order = order.Data as Order;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Order).State = EntityState.Modified;

            try
            {
                await business.Update(Order);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!OrderExists(Order.OrderId))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                throw;
            }

            return RedirectToPage("./Index");
        }

        //private bool OrderExists(Guid id)
        //{
        //    return _context.Orders.Any(e => e.OrderId == id);
        //}

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        await PopulateSelectListsAsync();
        //        return Page();
        //    }

        //    try
        //    {
        //        var productResult = await _product.GetById(ProductId);
        //        var product = productResult.Data as Product;

        //        if (product == null)
        //        {
        //            ModelState.AddModelError(string.Empty, "Failed to retrieve Product details.");
        //            await PopulateSelectListsAsync();
        //            return Page();
        //        }

        //        double discountPrice = 0;
        //        var finalPrice = Quantity * product.Price - discountPrice;

        //        Order.TotalPrice = finalPrice;

        //        var saveResult = await _business.Update(Order);
        //        if (saveResult.Status <= 0)
        //        {
        //            ModelState.AddModelError(string.Empty, "Failed to save Order.");
        //            await PopulateSelectListsAsync();
        //            return Page();
        //        }

        //        var orderDetail = new OrderDetail
        //        {
        //            OrderId = Order.OrderId,
        //            ProductId = ProductId,
        //            Quantity = Quantity,
        //            UnitPrice = product.Price,
        //            FinalPrice = finalPrice
        //        };

        //        var orderDetailSaveResult = await _detail.Update(orderDetail);
        //        if (orderDetailSaveResult.Status <= 0)
        //        {
        //            ModelState.AddModelError(string.Empty, "Failed to save OrderDetail.");
        //            await PopulateSelectListsAsync();
        //            return Page();
        //        }

        //        return RedirectToPage("./Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
        //        await PopulateSelectListsAsync();
        //        return Page();
        //    }
        //}

        //private async Task PopulateSelectListsAsync()
        //{
        //    await PopulateProductsSelectListAsync();
        //    await PopulateCompaniesSelectListAsync();
        //    await PopulateCustomersNameSelectListAsync();
        //    await PopulateCustomersAddressSelectListAsync();
        //}

        //private async Task PopulateProductsSelectListAsync()
        //{
        //    var productsResult = await _product.GetAll();
        //    if (productsResult.Status > 0 && productsResult.Data != null)
        //    {
        //        var productList = (IEnumerable<Product>)productsResult.Data;
        //        ProductsSelectList = new SelectList(productList, "ProductId", "Name");
        //    }
        //    else
        //    {
        //        ProductsSelectList = new SelectList(new List<Product>(), "ProductId", "Name");
        //    }
        //    ViewData["ProductsSelectList"] = ProductsSelectList;
        //}

        //private async Task PopulateCompaniesSelectListAsync()
        //{
        //    var companiesResult = await _company.GetAll();
        //    if (companiesResult.Status > 0 && companiesResult.Data != null)
        //    {
        //        var companyList = (IEnumerable<Company>)companiesResult.Data;
        //        CompaniesSelectList = new SelectList(companyList, "CompanyId", "CompanyName");
        //    }
        //    else
        //    {
        //        CompaniesSelectList = new SelectList(new List<Company>(), "CompanyId", "CompanyName");
        //    }
        //    ViewData["CompaniesSelectList"] = CompaniesSelectList;
        //}

        //private async Task PopulateCustomersNameSelectListAsync()
        //{
        //    var customersResult = await _customer.GetAll();
        //    if (customersResult.Status > 0 && customersResult.Data != null)
        //    {
        //        var customerList = (IEnumerable<Customer>)customersResult.Data;
        //        CustomersNameSelectList = new SelectList(customerList, "CustomerId", "CustomerFirstName");
        //    }
        //    else
        //    {
        //        CustomersNameSelectList = new SelectList(new List<Customer>(), "CustomerId", "CustomerFirstName");
        //    }
        //    ViewData["CustomersNameSelectList"] = CustomersNameSelectList;
        //}

        //private async Task PopulateCustomersAddressSelectListAsync()
        //{
        //    var customersResult = await _customer.GetAll();
        //    if (customersResult.Status > 0 && customersResult.Data != null)
        //    {
        //        var customerList = (IEnumerable<Customer>)customersResult.Data;
        //        CustomersAddressSelectList = new SelectList(customerList, "CustomerId", "CustomerAddress");
        //    }
        //    else
        //    {
        //        CustomersAddressSelectList = new SelectList(new List<Customer>(), "CustomerId", "CustomerAddress");
        //    }
        //    ViewData["CustomersAddressSelectList"] = CustomersAddressSelectList;
        //}
    }
}
