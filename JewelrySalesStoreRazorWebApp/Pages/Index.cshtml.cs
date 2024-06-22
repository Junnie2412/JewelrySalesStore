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
            // Luôn hiển thị banner và footer
            ViewData["ShowBanner"] = true;
            ViewData["ShowFooter"] = true;

            // Kiểm tra và thiết lập giá trị mặc định cho ShowNavBar
            int? showNavBar = HttpContext.Session.GetInt32("ShowNavBar");
            if (showNavBar == null)
            {
                HttpContext.Session.SetInt32("ShowNavBar", 0); // 0 = false
                ViewData["ShowNavBar"] = false;
            }
            else
            {
                ViewData["ShowNavBar"] = showNavBar == 1;
            }

            // Kiểm tra và thiết lập giá trị mặc định cho ShowLogin
            int? showLogin = HttpContext.Session.GetInt32("ShowLogin");
            if (showLogin == null)
            {
                HttpContext.Session.SetInt32("ShowLogin", 1); // 1 = true
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
                HttpContext.Session.SetInt32("ShowNavBar", 1); // 1 = true
                HttpContext.Session.SetInt32("ShowLogin", 0); // 0 = false
            }
            else
            {
                HttpContext.Session.SetInt32("ShowNavBar", 0); // 0 = false
                HttpContext.Session.SetInt32("ShowLogin", 1); // 1 = true
            }

            return RedirectToPage("/Index");
        }
    }
}
