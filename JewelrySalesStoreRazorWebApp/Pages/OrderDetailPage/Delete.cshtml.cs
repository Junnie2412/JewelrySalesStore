using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderDetailPage
{
    public class DeleteModel : PageModel
    {
        private readonly OrderDetailBusiness _business;
        private readonly OrderBusiness _order;

        public DeleteModel()
        {
            _business ??= new OrderDetailBusiness();
            _order ??= new OrderBusiness();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var detailResult = await _business.GetById(id);

            if (detailResult.Data == null)
            {
                return NotFound();
            }

            OrderDetail = detailResult.Data as OrderDetail;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var detailResult = await _business.GetById(id);
            var orderDetail = detailResult.Data as OrderDetail;

            if (orderDetail.IsActive == true)
            {
                ModelState.AddModelError(string.Empty, "Can't delete this order detail. You can only delete when status is false.");
                return Page();
            }

            var deleteDetailResult = await _business.DeleteById(id);
            if (deleteDetailResult.Status <= 0)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the order detail.");
                return Page();
            }

            if (orderDetail.OrderId != null)
            {
                await _order.DeleteById((Guid)orderDetail.OrderId);
            }

            return RedirectToPage("./Index");
        }
    }
}
