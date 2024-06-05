using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class PromotionModel : PageModel
    {
        private readonly PromotionBusiness _promotionBusiness = new PromotionBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Promotion Promotion { get; set; } = default;
        public List<Promotion> Promotions { get; set; } = new List<Promotion>();

        public void OnGet()
        {
            Promotions = this.GetPromotions();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to create promotion. Please check your input and try again!";
                return RedirectToPage();
            }

            Promotion.PromotionId = Guid.NewGuid();
            var isCreateSuccess = this.SavePromotion();
            if (isCreateSuccess != 0)
            {
                TempData["SuccessMessage"] = "Promotion created successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create promotion. Please try again later!";
            }
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate(Guid promotionId)
        {
            var result = _promotionBusiness.GetById(promotionId);
            if (result.Result.Data != null)
            {
                Promotion = result.Result.Data as Promotion;
            }
            else
            {
                TempData["ErrorMessage"] = result.Result.Message;
            }
            return new JsonResult(Promotion);
        }

        public IActionResult OnPostUpdate()
        {
            var promotion = GetById(Promotion.PromotionId);
            if (promotion == null)
            {
                TempData["ErrorMessage"] = "Promotion not found!";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to update promotion. Please check your input and try again!";
                return RedirectToPage();
            }

            promotion.PromotionName = Promotion.PromotionName;
            promotion.DiscountPercentage = Promotion.DiscountPercentage;
            promotion.StartDate = Promotion.StartDate;
            promotion.EndDate = Promotion.EndDate;

            var isUpdateSuccess = UpdatePromotion(promotion);
            if (isUpdateSuccess)
            {
                TempData["SuccessMessage"] = "Promotion updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update promotion. Please try again later!";
            }
            return RedirectToPage();
        }

        private bool UpdatePromotion(Promotion promotion)
        {
            var result = _promotionBusiness.Update(promotion);
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
            var promotion = GetById(Promotion.PromotionId);
            if (promotion == null)
            {
                TempData["ErrorMessage"] = "Promotion not found!";
                return RedirectToPage();
            }

            var isRemoveSuccess = _promotionBusiness.DeleteById(Promotion.PromotionId);
            if (isRemoveSuccess.Status > 0)
            {
                TempData["SuccessMessage"] = "Promotion removed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove promotion. Something went wrong!";
            }
            return RedirectToPage();
        }

        public List<Promotion> GetPromotions()
        {
            var result = _promotionBusiness.GetAll();
            if (result.Status > 0 && result.Result.Data != null)
            {
                return ((List<Promotion>)result.Result.Data).OrderByDescending(p => p.PromotionId).ToList();
            }
            return new List<Promotion>();
        }

        public Promotion GetById(Guid promotionId)
        {
            var result = _promotionBusiness.GetById(promotionId);
            if (result.Status > 0 && result.Result.Data != null)
            {
                return (Promotion)result.Result.Data;
            }
            return new Promotion();
        }

        private int SavePromotion()
        {
            var result = _promotionBusiness.Save(Promotion);
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
