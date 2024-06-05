using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class CompanyModel : PageModel
    {
        private readonly ICompanyBusiness _companyBusiness = new CompanyBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Company Company { get; set; } = default;
        public List<Company> Companies { get; set; } = new List<Company>();

        public void OnGet()
        {
            Companies = this.GetCompanies();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to create company. Please check your input and try again!";
                return RedirectToPage();
            }

            Company.CompanyId = Guid.NewGuid();
            var isCreateSuccess = this.SaveCompany();
            if (isCreateSuccess != 0)
            {
                TempData["SuccessMessage"] = "Company created successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create company. Please try again later!";
            }
            return RedirectToPage();
        }

        public IActionResult OnGetUpdate(Guid companyId)
        {
            var result = _companyBusiness.GetById(companyId);
            if (result.Result.Data != null)
            {
                Company = result.Result.Data as Company;
            }
            else
            {
                TempData["ErrorMessage"] = result.Result.Message;
            }
            return new JsonResult(Company);
        }

        public IActionResult OnPostUpdate()
        {
            var company = GetById(Company.CompanyId);
            if (company == null)
            {
                TempData["ErrorMessage"] = "Company not found!";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to update company. Please check your input and try again!";
                return RedirectToPage();
            }

            company.CompanyName = Company.CompanyName;
            company.CompanyPhone = Company.CompanyPhone;
            company.CompanyAddress = Company.CompanyAddress;
            company.CompanyDescription = Company.CompanyDescription;

            var isUpdateSuccess = UpdateCompany(company);
            if (isUpdateSuccess)
            {
                TempData["SuccessMessage"] = "Company updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update company. Please try again later!";
            }
            return RedirectToPage();
        }

        private bool UpdateCompany(Company company)
        {
            var result = _companyBusiness.Update(company);
            if (result.Status > 0)
            {
                Message = result.Result.Message;
                return true;
            }
            else
            {
                Message = "Error system";
                return false;
            }
        }

        public IActionResult OnPostDelete()
        {
            var company = GetById(Company.CompanyId);
            if (company == null)
            {
                TempData["ErrorMessage"] = "Company not found!";
                return RedirectToPage();
            }

            var isRemoveSuccess = _companyBusiness.DeleteById(Company.CompanyId);
            if (isRemoveSuccess.Status > 0)
            {
                TempData["SuccessMessage"] = "Company removed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove company. Something went wrong!";
            }
            return RedirectToPage();
        }

        public List<Company> GetCompanies()
        {
            var result = _companyBusiness.GetAll();
            if (result.Status > 0 && result.Result.Data != null)
            {
                return ((List<Company>)result.Result.Data).OrderByDescending(c => c.CompanyId).ToList();
            }
            return new List<Company>();
        }

        public Company GetById(Guid companyId)
        {
            var result = _companyBusiness.GetById(companyId);
            if (result.Status > 0 && result.Result.Data != null)
            {
                return (Company)result.Result.Data;
            }
            return new Company();
        }

        private int SaveCompany()
        {
            var result = _companyBusiness.Save(Company);
            if (result.Status > 0)
            {
                Message = result.Result.Message;
                return 1;
            }
            else
            {
                Message = "Error system";
                return 0;
            }
        }

        public string GetWelcomeMsg()
        {
            return "Welcome Razor Page Web Application";
        }
    }
}
