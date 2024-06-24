using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using System.ComponentModel.DataAnnotations;

namespace JewelrySalesStoreRazorWebApp.Pages.ProductPage
{
    public class CreateModel : PageModel
    {
        private readonly ProductBusiness _business;
        private readonly CategoryBusiness _category;
        private readonly PromotionBusiness _promotion;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(IWebHostEnvironment webHostEnvironment)
        {
            _business ??= new ProductBusiness();
            _category ??= new CategoryBusiness();
            _promotion ??= new PromotionBusiness();
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var listCategory = await _category.GetAll();
            if (listCategory != null && listCategory.Status > 0 && listCategory.Data != null)
            {
                Category = listCategory.Data as List<Category>;
            }

            var promotionList = await _promotion.GetAll();
            if (promotionList != null && promotionList.Status > 0 && promotionList.Data != null)
            {
                Promotion = promotionList.Data as List<Promotion>;
            }

            ViewData["CategoryId"] = new SelectList(Category, "CategoryId", "CategoryId");
            ViewData["PromotionId"] = new SelectList(Promotion, "PromotionId", "PromotionId");

            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public IList<Category> Category { get; set; } = default!;
        public IList<Promotion> Promotion { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Log file name and extension for debugging
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();

                    //// For debugging purposes: Log to console or add a breakpoint here
                    //Console.WriteLine($"FileName: {fileName}, Extension: {extension}");

                    if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("ImageFile", "Please upload an image file with a valid extension (png, jpg, jpeg).");
                        return Page();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        await ImageFile.CopyToAsync(memoryStream);
                        Product.Image = memoryStream.ToArray();
                    }
                }


                var result = await _business.Save(Product);
                if (result.Status <= 0)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the product.");
                    return Page();
                }
                else
                {
                    TempData["AlertMessage"] = "Create Product Successfully";
                    LogEvents.LogToFile("Create Product", "Create product Successfully", _webHostEnvironment);
                }

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
