using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;
using JewelrySalesStoreBusiness;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class DeleteModel : PageModel
    {
        private readonly OrderBusiness _business;
        private readonly OrderDetailBusiness _detail;

        public DeleteModel()
        {
            _business ??= new OrderBusiness();
            _detail = new OrderDetailBusiness();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        public OrderDetail OrderDetail { get; set; } = default!;

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _business.GetById(id);

            if (result.Data == null)
            {
                return NotFound();
            }

            Order = result.Data as Order;
            //var test = Order.OrderDetails;
            //var orderResult = await _detail.GetById(Order.OrderDetails.FirstOrDefault().OrderDetailId);
            //OrderDetail = orderResult.Data as OrderDetail;
            var demoDetail = _detail.GetOrderDetailsByOrderIdAsync(id);
            var resultOrderDetail = getOrderDetailByOrder(Order.OrderId) ;
            OrderDetail = resultOrderDetail.Result as OrderDetail;
            return Page();
        }
        public async Task <OrderDetail> getOrderDetailByOrder(Guid orderId)
        {
            var allOrderDetail = await _detail.GetAll();
            var listOrderDetail = allOrderDetail.Data as List<OrderDetail>;
            if(listOrderDetail != null)
            {
                foreach (var item in listOrderDetail)
                {
                    if (item.OrderId == Order.OrderId)
                    {
                        return item;
                    }
                }
                return null;
            }
            return null;
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            var resultOrder = await _business.GetById(id);
            var Order = resultOrder.Data as Order;

            if(Order.Status == true)
            {
                ModelState.AddModelError(string.Empty, "Can't delete this order. You can only delete the orders whose status is false");
                return Page();
            }
            var resultOrderDetail = getOrderDetailByOrder(id);
            OrderDetail = resultOrderDetail.Result as OrderDetail;
            var result1 = await _detail.DeleteById(OrderDetail.OrderDetailId);
            var result = await _business.DeleteById(id);

            if (result.Status <= 0)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the order.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
