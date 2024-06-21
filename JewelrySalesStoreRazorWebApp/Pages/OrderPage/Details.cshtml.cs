using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class DetailsModel : PageModel
    {
        private readonly OrderBusiness _business;

        public DetailsModel()
        {
            _business ??= new OrderBusiness();
        }

        public Order Order { get; set; } = default!;

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _business.GetById(id);
            if (result.Data == null)
            {
                return NotFound();
            }

            Order = result.Data as Order;
            return Page();
        }
    }
}
