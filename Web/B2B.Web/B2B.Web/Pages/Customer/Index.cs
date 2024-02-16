using B2B.Application.Interfaces;
using B2B.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace B2B.Web.Pages.Customer
{
    public class CustomerModel : PageModel
    {
        [BindProperty] 
        public Domain.Entities.Customer Input { get; set; }
        public List<Domain.Entities.Customer> Customers { get; set; }

        private readonly ICustomerRepository CustomerRepository;

        public CustomerModel(ICustomerRepository customerRepository)
        {
            CustomerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task OnGetAsync()
        {
           await RefreshCustomers();
        }

        private async Task RefreshCustomers()
        {
            try
            {
                Customers = await CustomerRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                RedirectToPage("/Error");
            }
        }
  }
}