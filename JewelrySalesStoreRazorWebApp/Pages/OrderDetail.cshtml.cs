using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class OrderDetailModel : PageModel
    {
        private readonly IOrderDetailBusiness _orderDetailBusiness = new OrderDetailBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default;
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public void OnGet()
        {
            OrderDetails = this.GetOrderDetails();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to create order detail. Please check your input and try again!";
                return RedirectToPage();
            }

            var isCreateSuccess = this.SaveOrderDetail();
            if (isCreateSuccess != 0)
            {
                TempData["SuccessMessage"] = "Order detail created successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create order detail. Please try again later!";
            }
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate(string orderDetailId)
        {
            var result = _orderDetailBusiness.GetById(orderDetailId);
            if (result.Result.Data != null)
            {
                OrderDetail = result.Result.Data as OrderDetail;
            }
            else
            {
                TempData["ErrorMessage"] = result.Result.Message;
            }
            return new JsonResult(OrderDetail);
        }

        public IActionResult OnPostUpdate()
        {
            var orderDetail = GetById(OrderDetail.OrderDetailId);
            if (orderDetail == null)
            {
                TempData["ErrorMessage"] = "Order detail not found!";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to update order detail. Please check your input and try again!";
                return RedirectToPage();
            }

            orderDetail.OrderId = OrderDetail.OrderId;
            orderDetail.Quantity = OrderDetail.Quantity;
            orderDetail.ProductId = OrderDetail.ProductId;
            orderDetail.UnitPrice = OrderDetail.UnitPrice;
            orderDetail.TotalPrice = OrderDetail.TotalPrice;

            var isUpdateSuccess = UpdateOrderDetail(orderDetail);
            if (isUpdateSuccess)
            {
                TempData["SuccessMessage"] = "Order detail updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update order detail. Please try again later!";
            }
            return RedirectToPage();
        }

        private bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            var result = _orderDetailBusiness.Update(orderDetail);
            if (result.Status > 0)
            {
                Message = result.Result.Message;
                return true;
            }
            else
            {
                Message = "Error system";
                return false;
            }
        }

        public IActionResult OnPostDelete()
        {
            var orderDetail = GetById(OrderDetail.OrderDetailId);
            if (orderDetail == null)
            {
                TempData["ErrorMessage"] = "Order detail not found!";
                return RedirectToPage();
            }

            var isRemoveSuccess = _orderDetailBusiness.DeleteById(OrderDetail.OrderDetailId);
            if (isRemoveSuccess.Status > 0)
            {
                TempData["SuccessMessage"] = "Order detail removed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove order detail. Something went wrong!";
            }
            return RedirectToPage();
        }

        public List<OrderDetail> GetOrderDetails()
        {
            var result = _orderDetailBusiness.GetAll();
            if (result.Status > 0 && result.Result.Data != null)
            {
                return ((List<OrderDetail>)result.Result.Data).OrderByDescending(c => c.OrderDetailId).ToList();
            }
            return new List<OrderDetail>();
        }

        public OrderDetail GetById(string orderDetailId)
        {
            var result = _orderDetailBusiness.GetById(orderDetailId);
            if (result.Status > 0 && result.Result.Data != null)
            {
                return (OrderDetail)result.Result.Data;
            }
            return new OrderDetail();
        }

        private int SaveOrderDetail()
        {
            var result = _orderDetailBusiness.Save(OrderDetail);
            if (result.Status > 0)
            {
                Message = result.Result.Message;
                return 1;
            }
            else
            {
                Message = "Error system";
                return 0;
            }
        }

        public string GetWelcomeMsg()
        {
            return "Welcome Razor Page Web Application";
        }
    }
}
