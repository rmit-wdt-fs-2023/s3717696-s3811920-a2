using MCBA_Admin.Models;

namespace MCBA_Admin.ViewModels;

public class CustomerBillViewModel
{
    public Customer Customer { get; set; }
    public Address CustomerAddress { get; set; }
    public List<BillPay> CustomerBills { get; set; }
}
