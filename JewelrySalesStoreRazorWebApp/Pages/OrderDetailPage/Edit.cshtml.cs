using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using JewelrySalesStoreBusiness.BusinessOrder;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderDetailPage
{
    public class EditModel : PageModel
    {
        private readonly OrderDetailBusiness _business;
        private readonly OrderBusiness _order;
        private readonly PromotionBusiness _promotion;

        public EditModel()
        {
            _business = new OrderDetailBusiness();
            _order = new OrderBusiness();
            _promotion = new PromotionBusiness();
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var orderDetailResult = await _business.GetById(id);
            if (orderDetailResult.Status <= 0 || orderDetailResult.Data == null)
            {
                return NotFound();
            }

            OrderDetail = orderDetailResult.Data as OrderDetail;
            if (OrderDetail == null)
            {
                return NotFound();
            }

            var orderResult = await _order.GetById(OrderDetail.OrderId ?? Guid.Empty);
            if (orderResult.Status <= 0 || orderResult.Data == null)
            {
                return NotFound();
            }

            OrderOriginal = orderResult.Data as Order;

            await PopulatePromotionsSelectListAsync();

            if (PromotionId != Guid.Empty)
            {
                PromotionId = PromotionId;
                PromotionCode = PromotionCode;
            }

            return Page();
        }

        public SelectList PromotionsSelectList { get; set; }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        public Order OrderOriginal { get; set; }

        [BindProperty]
        public Guid PromotionId { get; set; }

        [BindProperty]
        public string PromotionCode { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulatePromotionsSelectListAsync();
                return Page();
            }

            try
            {
                if (PromotionId != Guid.Empty)
                {
                    var promotionResult = await _promotion.GetById(PromotionId);
                    var promotion = promotionResult.Data as Promotion;
                    if (promotion == null || !promotion.IsActive == true || !promotion.PromotionCode.Equals(PromotionCode, StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError("PromotionCode", "Invalid or inactive promotion code.");
                        await PopulatePromotionsSelectListAsync();
                        return Page();
                    }

                    OrderDetail.DiscountPrice = OrderDetail.Quantity * OrderDetail.UnitPrice * (promotion.DiscountPercentage / 100);
                    OrderDetail.FinalPrice = (OrderDetail.Quantity * OrderDetail.UnitPrice) - OrderDetail.DiscountPrice;
                }
                else
                {
                    OrderDetail.DiscountPrice = 0;
                    OrderDetail.FinalPrice = OrderDetail.Quantity * OrderDetail.UnitPrice;
                }

                var updateDetailResult = await _business.Update(OrderDetail);
                if (updateDetailResult.Status <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update OrderDetail.");
                    await PopulatePromotionsSelectListAsync();
                    return Page();
                }

                await UpdateOrderAsync(OrderDetail.OrderId.Value);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                await PopulatePromotionsSelectListAsync();
                return Page();
            }
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

        private async Task UpdateOrderAsync(Guid orderId)
        {
            var orderResult = await _order.GetById(orderId);
            if (orderResult.Status <= 0 || orderResult.Data == null)
            {
                ModelState.AddModelError(string.Empty, "Failed to retrieve Order.");
                return;
            }

            var order = orderResult.Data as Order;
            if (order != null)
            {
                order.TotalPrice = OrderDetail.FinalPrice;
                order.Status = OrderDetail.IsActive;
                var updateResult = await _order.Update(order);
                if (updateResult.Status <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update Order.");
                }
            }
        }

        public async Task<JsonResult> OnGetCalculateFinalPrice(Guid promotionId, int quantity, double unitPrice)
        {
            double finalPrice = quantity * unitPrice;
            double discountPrice = 0;
            double finalDiscountPrice = finalPrice;

            if (promotionId != Guid.Empty)
            {
                var promotionResult = await _promotion.GetById(promotionId);
                var promotion = promotionResult.Data as Promotion;
                if (promotion != null && promotion.IsActive == true)
                {
                    discountPrice = (double)(finalPrice * (promotion.DiscountPercentage / 100));
                    finalDiscountPrice = finalPrice - discountPrice;
                }
            }

            return new JsonResult(new
            {
                discountPrice = discountPrice,
                finalPrice = finalDiscountPrice
            });
        }
    }
}
