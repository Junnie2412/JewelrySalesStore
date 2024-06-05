using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;

namespace JewelrySalesStore.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*var categoryBusiness = new CategoryBusiness();
            var categoryList = categoryBusiness.GetAll();

            if (categoryList.Status > 0 && categoryList.Result.Data != null)
            {
                var categories = (List<Category>)categoryList.Result.Data;

                if (categories != null && categories.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        Console.WriteLine(category.CategoryId);
                    }
                }


            }
            else
            {
                Console.WriteLine("Get All Category fail");
            }

            Console.WriteLine("DAO.GetById(code)");
            var categoryResult = categoryBusiness.GetById("00cc640c-1dc4-42d6-9349-04df92cef402");

            if (categoryResult.Status > 0 && categoryResult.Result.Data != null)
            {
                var category = (Category)categoryResult.Result.Data;
                Console.WriteLine(category.Name);
            }

            Console.WriteLine("DAO.Save(code)");
            categoryResult = categoryBusiness.GetById("00cc640c-1dc4-42d6-9349-04df92cef402");
            if (categoryResult.Status > 0 && categoryResult.Result.Data != null)
            {
                Console.WriteLine("This category is exist");
            }
            else
            {
                var item = new Category()
                {
                    CategoryId = Guid.Parse("00cc640c-1dc4-42d6-9349-04df92cef402"),
                    Name = "Dây chuyền",
                };

                var result = categoryBusiness.Save(item);
                Console.WriteLine(result.Result.Message);
            }
            */

            var productBusiness = new ProductBusiness();
            var productList = productBusiness.GetAll();

            if (productList.Status > 0 && productList.Result.Data != null)
            {
                var products = (List<Product>)productList.Result.Data;

                if (products != null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        Console.WriteLine(product.ProductId);
                    }
                }
            }
            else
            {
                Console.WriteLine("Get All Product fail");
            }

            Console.WriteLine("DAO.GetById(code)");
            var productResult = productBusiness.GetById("040d52a4-c728-40d3-b6d6-286c8a1057cf");

            if (productResult.Status > 0 && productResult.Result.Data != null)
            {
                var product = (Product)productResult.Result.Data;
                Console.WriteLine(product.Name);
            }

            Console.WriteLine("DAO.Save(code)");
            productResult = productBusiness.GetById("6c0424ef-7d98-4dea-85d0-8ba97db4f13d");
            if (productResult.Status > 0 && productResult.Result.Data != null)
            {
                Console.WriteLine("This product is exist");
            }
            else
            {
                var item = new Product()
                {
                    ProductId = Guid.Parse("6c0424ef-7d98-4dea-85d0-8ba97db4f13d"),
                    Color = "Yellow",
                    Name = "VictoriaII",
                    BarCode = Guid.Parse("e6088a60-ae2f-48c1-9ffa-b99217c850b3"),
                    Wage = 65.599,
                    Weight = 23.5,
                    Image = null,
                    Price = 45.600,
                    PromotionId = Guid.Parse("02febe4a-47aa-46d0-a5df-e041eceabde4"),
                    Status = true,
                    CategoryId = Guid.Parse("3aa6753c-8ee5-4926-a6fb-741b4df051cd")
                };

                var result = productBusiness.Save(item);
                Console.WriteLine(result.Result.Message);
            }

        }
    }
}
