using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Msg { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            try
            {
                if (Username == "admin" && Password == "123")
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    Msg = "Invalid";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                Msg = ex.Message;
                return Page();
            }
        }
    }
}
