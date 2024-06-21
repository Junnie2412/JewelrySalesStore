using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JewelrySalesStoreRazorWebApp.Pages.ProductPage
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ProductBusiness _business;
        private readonly CategoryBusiness _category;
        private readonly PromotionBusiness _promotion;

        public EditModel(IWebHostEnvironment environment)
        {
            _environment = environment;
            _business ??= new ProductBusiness();
            _category ??= new CategoryBusiness();
            _promotion ??= new PromotionBusiness();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public IList<Category> Category { get; set; } = default!;
        public IList<Promotion> Promotion { get; set; } = default!;
        [BindProperty]
        public IFormFile ImageFile { get; set; } = default!;
        [BindProperty]
        public bool HasImage { get; set; }
        [BindProperty]
        public bool RemoveImage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = await _business.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

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

            Product = product.Data as Product;
            HasImage = Product.Image != null && Product.Image.Length > 0;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            try
            {
                if (RemoveImage)
                {
                    // Handle case where user wants to remove current image
                    Product.Image = null;
                }
                else if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Handle case where user uploads a new image
                    // Example: Save the new image file to a location
                    var uploadDir = Path.Combine(_environment.WebRootPath, "uploads");

                    // Ensure the directory exists
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // Update Product.Image with the new file path or byte array as needed
                    // Example: Product.Image = await ReadFileAsync(filePath);
                }

                await _business.Update(Product);
            }
            catch (Exception e)
            {

            }

            return RedirectToPage("./Index");
        }

        //private bool CategoryExists(Guid id)
        //{
        //    return _context.Categories.Any(e => e.CategoryId == id);
        //}
    }
}
