using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductBusiness _productBusiness = new ProductBusiness();
        private readonly IOrderDetailBusiness _orderDetailBusiness = new OrderDetailBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Product Product { get; set; } = default;
        public List<Product> Products { get; set; } = new List<Product>();
        [BindProperty]
        public Order Order { get; set; }
        [BindProperty]
        public OrderDetail OrderDetail { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
       
        public void OnGet()
        {
            Products = this.GetProducts();
            OrderDetails = this.GetOrderDetails();
        }

        public IActionResult OnPost()
        {
            this.SaveProduct();
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate(Guid productId)
        {
            var result = _productBusiness.GetById(productId);
            if (result.Result.Data != null)
            {
                Product = result.Result.Data as Product;

            }
            else
            {

                TempData["ErrorMessage"] = result.Result.Message;
            }
            return new JsonResult(Product);
        }

        public IActionResult OnPostUpdate()
        {

            var product = GetById(Product.ProductId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found!";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to update product. Please check your input and try again!";
                return RedirectToPage();
            }


            product.Name = Product.Name;
            product.Image = Product.Image;
            product.Weight = Product.Weight;
            product.Color = Product.Color;
            product.Price = Product.Price;
            product.Status = Product.Status;
            product.PromotionId = Product.PromotionId;

            var isUpdateSuccess = Updateproduct(product);
            if (isUpdateSuccess)
            {
                TempData["SuccessMessage"] = "Product updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update Product. Please try again later!";
            }

            return RedirectToPage();

        }

        private bool Updateproduct(Product product)
        {
            var productResult = _productBusiness.Update(product);
            if (productResult != null)
            {
                this.Message = productResult.Result.Message;
                return true;
            }
            else
            {
                this.Message = "Error system";
                return false;
            }
        }

        public Product GetById(Guid productId)
        {
            var productResult = _productBusiness.GetById(productId);
            if (productResult.Status > 0 && productResult.Result.Data != null)
            {
                var product = (Product)productResult.Result.Data;
                return product;
            }
            return new Product();
        }

        public IActionResult OnGetOrderDetail(Guid productId)
        {
            var result = _productBusiness.GetById(productId);
            if (result.Result.Data != null)
            {
                Product = result.Result.Data as Product;

            }
            else
            {

                TempData["ErrorMessage"] = result.Result.Message;
            }
            return new JsonResult(Product);
        }

        public IActionResult OnPostOrderDetail()
        {
            this.SaveOrderDetail();
            return RedirectToPage();
        }

        private void SaveOrderDetail()
        {
            var OrderDetailResult = _orderDetailBusiness.Save(this.OrderDetail);

            if (OrderDetailResult != null)
            {
                this.Message = OrderDetailResult.Result.Message;
            }
            else
            {
                this.Message = "Error system";
            }
        }

        public IActionResult OnPostDelete()
        {
            var product = GetById(Product.ProductId);
            if (product == null)
            {
                TempData["ErrorMessage"] = "Product not found!";
                return RedirectToPage();
            }

            var isRemoveSuccess = _productBusiness.DeleteById(Product.ProductId);
            if (isRemoveSuccess != null)
            {
                TempData["SuccessMessage"] = "Product Remove successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove Product. Something wrong!";
            }
            return RedirectToPage();
        }

        private List<OrderDetail> GetOrderDetails()
        {
            var OrderDetailResult = _orderDetailBusiness.GetAll();
            if(OrderDetailResult.Status > 0 && OrderDetailResult.Result.Data != null)
            {
                var orderdetails = (List<OrderDetail>)OrderDetailResult.Result.Data;
                return orderdetails;
            }
            return new List<OrderDetail>();
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
