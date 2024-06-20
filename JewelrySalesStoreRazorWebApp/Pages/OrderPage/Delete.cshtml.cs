using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JewelrySalesStoreData.Models;
using JewelrySalesStoreBusiness.BusinessOrder;

namespace JewelrySalesStoreRazorWebApp.Pages.OrderPage
{
    public class DeleteModel : PageModel
    {
        private readonly OrderBusiness _business;

        public DeleteModel()
        {
            _business ??= new OrderBusiness();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _business.DeleteById(id);

            if (result.Status <= 0)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the order.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
