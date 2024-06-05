using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductBusiness _productBusiness = new ProductBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Product Product { get; set; } = default;
        public List<Product> Products { get; set; } = new List<Product>();
        public void OnGet()
        {
            Products = this.GetProducts();
        }

        public IActionResult OnPost()
        {
            this.SaveProduct();
            return RedirectToPage();
        }

        public void OnDelete()
        {
        }

        private List<Product> GetProducts()
        {
            var ProductResult = _productBusiness.GetAll();

            if (ProductResult.Status > 0 && ProductResult.Result.Data != null)
            {
                var currencies = (List<Product>)ProductResult.Result.Data;
                return currencies;
            }
            return new List<Product>();
        }

        private void SaveProduct()
        {
            var ProductResult = _productBusiness.Save(this.Product);

            if (ProductResult != null)
            {
                this.Message = ProductResult.Result.Message;
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
    }
}
