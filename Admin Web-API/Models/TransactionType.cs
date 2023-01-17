using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Admin.Models;

public enum TransactionType
{
    Transfer,
    Withdraw,
    Deposit,
    ServiceCharge,
    BillPay
}