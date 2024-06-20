using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using JewelrySalesStoreBusiness.BusinessOrder;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderDetailPage
{
    public class EditModel : PageModel
    {
        private readonly OrderDetailBusiness _business;
        private readonly OrderBusiness _order;

        public EditModel()
        {
            _business = new OrderDetailBusiness();
            _order = new OrderBusiness();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetailResult = await _business.GetById(id);
            if (orderDetailResult.Status <= 0 || orderDetailResult.Data == null)
            {
                return NotFound();
            }

            OrderDetail = orderDetailResult.Data as OrderDetail;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                //OrderDetail.FinalPrice = (OrderDetail.Quantity * OrderDetail.UnitPrice) - OrderDetail.DiscountPrice;

                //var updateDetailResult = await _business.Update(OrderDetail);
                //await _business.Update(OrderDetail);
                //if (updateDetailResult.Status <= 0)
                //{
                //    ModelState.AddModelError(string.Empty, "Failed to update OrderDetail.");
                //    return Page();
                //}

                //var orderResult = await _order.GetById((Guid)OrderDetail.OrderId);
                //var order = orderResult.Data as Order;
                //if (order != null)
                //{
                //    order.TotalPrice = OrderDetail.FinalPrice;
                //    await _order.Update(order);
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Failed to update Order TotalPrice.");
                //    return Page();
                //}

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}
