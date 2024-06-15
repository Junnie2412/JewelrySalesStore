using JewelrySalesStoreBusiness.BusinessOrder;
using JewelrySalesStoreData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IOrderBusiness _orderBusiness = new OrderBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Order Order { get; set; } = default;
        public List<Order> Orders { get; set; } = new List<Order>();

        public void OnGet()
        {
            Orders = this.GetOrders();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to create order. Please check your input and try again!";
                return RedirectToPage();
            }

            Order.OrderId = Guid.NewGuid();
            var isCreateSuccess = this.SaveOrder();
            if (isCreateSuccess != 0)
            {
                TempData["SuccessMessage"] = "Order created successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create order. Please try again later!";
            }
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate(Guid orderId)
        {
            var result = _orderBusiness.GetById(orderId);
            if (result.Result.Data != null)
            {
                Order = result.Result.Data as Order;
            }
            else
            {
                TempData["ErrorMessage"] = result.Result.Message;
            }
            return new JsonResult(Order);
        }

        public IActionResult OnPostUpdate()
        {
            var order = GetById(Order.OrderId);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found!";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to update order. Please check your input and try again!";
                return RedirectToPage();
            }

            order.CustomerId = Order.CustomerId;
            order.Date = Order.Date;
            order.TotalPrice = Order.TotalPrice;
            order.PaymentMethod = Order.PaymentMethod;

            var isUpdateSuccess = UpdateOrder(order);
            if (isUpdateSuccess)
            {
                TempData["SuccessMessage"] = "Order updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update order. Please try again later!";
            }
            return RedirectToPage();
        }

        private bool UpdateOrder(Order order)
        {
            var result = _orderBusiness.Update(order);
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
            var order = GetById(Order.OrderId);
            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found!";
                return RedirectToPage();
            }

            var isRemoveSuccess = _orderBusiness.DeleteById(Order.OrderId);
            if (isRemoveSuccess.Status > 0)
            {
                TempData["SuccessMessage"] = "Order removed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove order. Something went wrong!";
            }
            return RedirectToPage();
        }

        public List<Order> GetOrders()
        {
            var result = _orderBusiness.GetAll();
            if (result.Status > 0 && result.Result.Data != null)
            {
                return ((List<Order>)result.Result.Data).OrderByDescending(o => o.OrderId).ToList();
            }
            return new List<Order>();
        }

        public Order GetById(Guid orderId)
        {
            var result = _orderBusiness.GetById(orderId);
            if (result.Status > 0 && result.Result.Data != null)
            {
                return (Order)result.Result.Data;
            }
            return new Order();
        }

        private int SaveOrder()
        {
            var result = _orderBusiness.Save(Order);
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
