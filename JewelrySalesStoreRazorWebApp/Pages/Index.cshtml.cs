using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

     
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            ViewData["ShowBanner"] = true;
            ViewData["ShowFooter"] = true;

            //  ShowNavBar
            int? showNavBar = HttpContext.Session.GetInt32("ShowNavBar");
            if (showNavBar == null)
            {
                ViewData["ShowNavBar"] = false;
            }
            else
            {
                ViewData["ShowNavBar"] = showNavBar == 1;
            }

            // ShowLogin
            int? showLogin = HttpContext.Session.GetInt32("ShowLogin");
            if (showLogin == null)
            {
                ViewData["ShowLogin"] = true;
            }
            else
            {
                ViewData["ShowLogin"] = showLogin == 1;
            }
        }

        public IActionResult OnPostLogin(bool? showNavBar)
        {
            if (Username == "admin" && Password == "123")
            {
                HttpContext.Session.SetInt32("ShowNavBar", 1);
                HttpContext.Session.SetInt32("ShowLogin", 0);
            }
            else
            {
                HttpContext.Session.SetInt32("ShowNavBar", 0);
                HttpContext.Session.SetInt32("ShowLogin", 1);
            }

            return RedirectToPage("/Index");



            /*
                  if (Account.AccountRole == 1 || Account.AccountRole == 2)
                    {
                        HttpContext.Session.SetInt32("User", (int)Account.AccountRole);
                    }  

                   //Check role
                    var role = HttpContext.Session.GetInt32("User");
                    if (role != 1 || role == null)
                    {
                        HttpContext.Session.Clear();
                        return RedirectToPage("/Login");
                    }
                */
        }
    }
}
