using JewelrySalesStoreBusiness;
using JewelrySalesStoreBusiness.BusinessOrder;
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
            var categoryResult = categoryBusiness.GetById(Guid.Parse("00cc640c-1dc4-42d6-9349-04df92cef402"));

            if (categoryResult.Status > 0 && categoryResult.Result.Data != null)
            {
                var category = (Category)categoryResult.Result.Data;
                Console.WriteLine(category.Name);
            }

            Console.WriteLine("DAO.Save(code)");
            categoryResult = categoryBusiness.GetById(Guid.Parse("00cc640c-1dc4-42d6-9349-04df92cef402"));
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
            var productResult = productBusiness.GetById(Guid.Parse("040d52a4-c728-40d3-b6d6-286c8a1057cf"));

            if (productResult.Status > 0 && productResult.Result.Data != null)
            {
                var product = (Product)productResult.Result.Data;
                Console.WriteLine(product.Name);
            }

            //Console.WriteLine("DAO.Save(code)");
            //productResult = productBusiness.GetById(Guid.Parse("B274683B-040F-4C20-97C5-2093589D8E34"));
            //if (productResult.Status > 0 && productResult.Result.Data != null)
            //{
            //    Console.WriteLine("This product is exist");
            //}
            //else
            //{
            //    var item = new Product()
            //    {
            //        ProductId = Guid.Parse("B274683B-040F-4C20-97C5-2093589D8E34"),
            //        PromotionId = Guid.Parse("A57B0140-4EE3-4623-8876-0ACBACE995D5"),
            //        CategoryId = Guid.Parse("00CC640C-1DC4-42D6-9349-04DF92CEF402")
                
            //    };

            //    var result = productBusiness.Save(item);
            //    Console.WriteLine(result.Result.Message);
            //}

            Console.WriteLine("DAO.Remove(code)");




                var result = productBusiness.DeleteById(Guid.Parse("19179C62-7721-440C-96B4-36FBCFD2C6F6"));
                Console.WriteLine(result.Result.Message);
            

        }
    }
}
