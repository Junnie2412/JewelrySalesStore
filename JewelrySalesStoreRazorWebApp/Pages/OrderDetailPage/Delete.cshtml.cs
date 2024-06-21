using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using JewelrySalesStoreBusiness.BusinessOrder;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderDetailPage
{
    public class DeleteModel : PageModel
    {
        private readonly OrderDetailBusiness _business;
        private readonly OrderBusiness _order ;

        public DeleteModel()
        {
            _business ??= new OrderDetailBusiness();
            _order ??= new OrderBusiness();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _business.GetById(id);

            if (orderDetail == null)
            {
                return NotFound();
            }
            else
            {
                OrderDetail = orderDetail.Data as OrderDetail;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var detailResult = await _business.GetById(id);
            var detail = detailResult.Data as OrderDetail;

            if (OrderDetail.IsActive == true)
            {
                ModelState.AddModelError(string.Empty, "Can't delete this order. You can only delete the orders whose status is false");
                return Page();
            }

            var result = await _business.DeleteById(id);

            if (result.Status <= 0)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the Order detail.");
                return Page();
            }

            var orderResult = await _order.GetById(OrderDetail.OrderId ?? Guid.Empty);
            var order = orderResult.Data as Order;
            if (order != null)
            {
                order.TotalPrice = OrderDetail.FinalPrice;
                await _order.DeleteById(order.OrderId);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete Order.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
