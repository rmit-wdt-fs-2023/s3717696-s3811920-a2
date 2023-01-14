using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCBA_Web.Models;

public class BillPay
{
    [Required]
    [Key]
    public int BillPayID { get; set; }

    [ForeignKey("AccountNumber")]
    public int AccountNumber { get; set; }

    [ForeignKey("PayeeID")]
    public int PayeeID { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be positive")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime ScheduleTimeUtc { get; set; }

    [Required]
    public PaymentPeriodType PaymentPeriod { get; set; }

    public virtual Account Account { get; set; }
    public virtual Payee Payee { get; set; }
}
