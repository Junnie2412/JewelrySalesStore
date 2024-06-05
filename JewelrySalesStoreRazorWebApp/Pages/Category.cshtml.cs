using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class CategoryModel : PageModel
    {
        private readonly ICategoryBusiness _categoryBusiness = new CategoryBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Category Category { get; set; } = default;
        public List<Category> Categories { get; set; } = new List<Category>();
        public void OnGet()
        {
            Categories = this.GetCategories();
        }

        public IActionResult OnPost()
        {
            this.SaveCategory();
            return RedirectToPage();
        }

        public void OnDelete()
        {
        }

        public IActionResult OnGetUpdate(Guid categoryId)
        {
            var result = _categoryBusiness.GetById(categoryId);
            if (result.Result.Data != null)
            {
                Category = result.Result.Data as Category;

            }
            else
            {

                TempData["ErrorMessage"] = result.Result.Message;
            }
            return new JsonResult(Category);
        }

        public IActionResult OnPostUpdate()
        {

            var category = GetById(Category.CategoryId);
            if (category == null)
            {
                TempData["ErrorMessage"] = "Category not found!";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to update category. Please check your input and try again!";
                return RedirectToPage();
            }


            category.Name = Category.Name;
            

            var isUpdateSuccess = UpdateCategory(category);
            if (isUpdateSuccess)
            {
                TempData["SuccessMessage"] = "Category updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update category. Please try again later!";
            }

            return RedirectToPage();

        }

        public IActionResult OnPostDelete()
        {
            var customer = GetById(Category.CategoryId);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Category not found!";
                return RedirectToPage();
            }

            var isRemoveSuccess = _categoryBusiness.DeleteById(Category.CategoryId);
            if (isRemoveSuccess != null)
            {
                TempData["SuccessMessage"] = "Category Remove successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove category. Something wrong!";
            }
            return RedirectToPage();
        }

        private List<Category> GetCategories()
        {
            var CategoryResult = _categoryBusiness.GetAll();

            if (CategoryResult.Status > 0 && CategoryResult.Result.Data != null)
            {
                var currencies = (List<Category>)CategoryResult.Result.Data;
                return currencies;
            }
            return new List<Category>();
        }

        private void SaveCategory()
        {
            var CategoryResult = _categoryBusiness.Save(this.Category);

            if (CategoryResult != null)
            {
                this.Message = CategoryResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        public string GetWelcomeMsg()
        {
            return "Welcome Razor Page Web Application";
        }

        public Category GetById(Guid categoryId)
        {
            var categoryResult = _categoryBusiness.GetById(categoryId);
            if (categoryResult.Status > 0 && categoryResult.Result.Data != null)
            {
                var category = (Category)categoryResult.Result.Data;
                return category;
            }
            return new Category();
        }

        private bool UpdateCategory(Category category)
        {
            var categoryResult = _categoryBusiness.Update(category);
            if (categoryResult != null)
            {
                this.Message = categoryResult.Result.Message;
                return true;
            }
            else
            {
                this.Message = "Error system";
                return false;
            }
        }
    }
}
