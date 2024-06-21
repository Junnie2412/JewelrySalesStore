using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using JewelrySalesStoreBusiness.BusinessOrder;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [BindProperty]
        public OrderDetail OrderDetail { get; set; }

        [BindProperty]
        public Guid? PromotionId { get; set; }

        [BindProperty]
        public string PromotionCode { get; set; }

        public SelectList PromotionsSelectList { get; set; }

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

            await PopulatePromotionsSelectListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulatePromotionsSelectListAsync();
                return Page();
            }

            try
            {
                var promotionsResult = await _promotion.GetById((Guid)PromotionId);
                if (promotionsResult.Status > 0 && promotionsResult.Data != null)
                {
                    var promotion = (Promotion)promotionsResult.Data;
                    OrderDetail.DiscountPrice = OrderDetail.Quantity * OrderDetail.UnitPrice * (promotion.DiscountPercentage ?? 0) / 100;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid promotion selection.");
                    await PopulatePromotionsSelectListAsync();
                    return Page();
                }

                OrderDetail.FinalPrice = (OrderDetail.Quantity * OrderDetail.UnitPrice) - OrderDetail.DiscountPrice;

                var updateDetailResult = await _business.Update(OrderDetail);
                if (updateDetailResult.Status <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update OrderDetail.");
                    await PopulatePromotionsSelectListAsync();
                    return Page();
                }

                var orderResult = await _order.GetById(OrderDetail.OrderId ?? Guid.Empty);
                var order = orderResult.Data as Order;
                if (order != null)
                {
                    order.TotalPrice = OrderDetail.FinalPrice;
                    await _order.Update(order);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update Order TotalPrice.");
                    await PopulatePromotionsSelectListAsync();
                    return Page();
                }

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
    }
}
