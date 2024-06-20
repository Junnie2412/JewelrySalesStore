using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public bool ShowNavBar { get; set; } = false;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(bool? showNavBar)
        {
           
            ViewData["ShowNavBar"] = showNavBar ?? false; 
            ViewData["ShowBanner"] = true;
            ViewData["ShowFooter"] = true;
        }

        public IActionResult OnPostLogin(bool? showNavBar)
        {
            if (Username == "admin" && Password == "123")
            {
                ShowNavBar = true;
            }
            else
            {
                ShowNavBar = false;
            }

            ViewData["ShowBanner"] = true;
            ViewData["ShowFooter"] = true;
            ViewData["ShowNavBar"] = ShowNavBar;

            return Page();
        }
    }
}
