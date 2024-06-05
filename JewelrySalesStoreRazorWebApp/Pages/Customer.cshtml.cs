using JewelrySalesStoreBusiness;
using JewelrySalesStoreData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Dynamic;

namespace JewelrySalesStoreRazorWebApp.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerBusiness _customerBusiness = new CustomerBusiness();
        public string Message { get; set; } = default;
        [BindProperty]
        public Customer Customer { get; set; } = default;
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public void OnGet()
        {
            Customers = this.GetCustomers();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to create customer. Please check your input and try again!";
                return RedirectToPage();
            }

            var isCreateSuccess = this.SaveCustomer();
            if (isCreateSuccess != 0)
            {
                TempData["SuccessMessage"] = "Customer created successfully!";
            }
            return RedirectToPage();
        }


        public IActionResult OnGetUpdate(Guid customerId)
        {
            var result = _customerBusiness.GetById(customerId);
            if (result.Result.Data != null)
            {
                Customer = result.Result.Data as Customer;

            }
            else
            {

                TempData["ErrorMessage"] = result.Result.Message;
            }
            return new JsonResult(Customer);
        }

        public IActionResult OnPostUpdate()
        {

            var customer = GetById(Customer.CustomerId);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Customer not found!";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Failed to update customer. Please check your input and try again!";
                return RedirectToPage();
            }


            customer.CustomerName = Customer.CustomerName;
            customer.CustomerPhone = Customer.CustomerPhone;
            customer.CustomerBirthDate = Customer.CustomerBirthDate;
            customer.CustomerAddress = Customer.CustomerAddress;
            customer.CustomerPoint = Customer.CustomerPoint;

            var isUpdateSuccess = UpdateCustomer(customer);
            if (isUpdateSuccess)
            {
                TempData["SuccessMessage"] = "Customer updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update customer. Please try again later!";
            }

            return RedirectToPage();

        }

        private bool UpdateCustomer(Customer customer)
        {
            var customerResult = _customerBusiness.Update(customer);
            if (customerResult != null)
            {
                this.Message = customerResult.Result.Message;
                return true;
            }
            else
            {
                this.Message = "Error system";
                return false;
            }
        }


        public IActionResult OnPostDelete()
        {
            var customer = GetById(Customer.CustomerId);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Customer not found!";
                return RedirectToPage();
            }

            var isRemoveSuccess = _customerBusiness.DeleteById(Customer.CustomerId);
            if (isRemoveSuccess != null)
            {
                TempData["SuccessMessage"] = "Customer Remove successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to remove customer. Something wrong!";
            }
            return RedirectToPage();
        }

        public List<Customer> GetCustomers()
        {
            var customerResult = _customerBusiness.GetAll();
            if (customerResult.Status > 0 && customerResult.Result.Data != null)
            {
                var customers = (List<Customer>)customerResult.Result.Data;
                return customers;
            }

            return new List<Customer>();
        }

        public Customer GetById(Guid customerId)
        {
            var customerResult = _customerBusiness.GetById(customerId);
            if (customerResult.Status > 0 && customerResult.Result.Data != null)
            {
                var customer = (Customer)customerResult.Result.Data;
                return customer;
            }
            return new Customer();
        }

        private int SaveCustomer()
        {
            var customerResult = _customerBusiness.Save(this.Customer);

            if (customerResult != null)
            {
                this.Message = customerResult.Result.Message;
                return 1;
            }
            else
            {
                this.Message = "Error system";
                return 0;
            }
        }



        public string GetWelcomeMsg()
        {
            return "Welcome Razor Page Web Application";
        }
    }
}
