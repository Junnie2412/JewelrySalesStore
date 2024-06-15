using JewelrySalesStoreBusines;
using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;

namespace JewelrySalesStoreConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var PromotionBusiness = new PromotionBusiness();
            var PromotionList = PromotionBusiness.GetAll();

            if (PromotionList.Status > 0 && PromotionList.Result.Data != null)
            {
                var promotions = (List<Promotion>)PromotionList.Result.Data;

                if (promotions != null && promotions.Count > 0)
                {
                    foreach (var Promotion in promotions)
                    {
                        Console.WriteLine(Promotion.PromotionId);
                    }
                }


            }
            else
            {
                Console.WriteLine("Get All Promotion fail");
            }
            Guid a = Guid.Parse("b4ecfbd2-f864-4f0f-86f2-f961a145f790");

            Console.WriteLine("DAO.GetById(code)");
            var PromotionResult = PromotionBusiness.GetById(a);

            if (PromotionResult.Status > 0 && PromotionResult.Result.Data != null)
            {
                var Promotion = (Promotion)PromotionResult.Result.Data;
                Console.WriteLine(Promotion.PromotionName);
            }

            Console.WriteLine("DAO.Save(code)");
            PromotionResult = PromotionBusiness.GetById(a);
            if (PromotionResult.Status > 0 && PromotionResult.Result.Data != null)
            {
                Console.WriteLine("This Promotion is exist");
            }
            else
            {
                var item = new Promotion()
                {
                    PromotionId = Guid.Parse("b4ecfbd2-f864-4f0f-86f2-f961a145f790"),
                    PromotionName = "Korea",
                };
                var result = PromotionBusiness.Save(item);
                Console.WriteLine(result.Result.Message);
            }

        }
    }
}