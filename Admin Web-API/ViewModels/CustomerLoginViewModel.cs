using MCBA_Admin.Models;

namespace MCBA_Admin.ViewModels;

public class CustomerLoginViewModel
{
    public List<Customer> Customers { get; set; }
    public List<Login> Logins { get; set; }
}
