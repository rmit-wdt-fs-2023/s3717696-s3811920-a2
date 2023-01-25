using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCBA_Web.Models;
public class BillPay
{
    [Required]
    [Key]
    [Display(Name = "BillPay ID")]
    public int BillPayID { get; set; }

    public int AccountNumber { get; set; }

    [ForeignKey("PayeeID")]
    [Display(Name = "Payee ID")]
    public int PayeeID { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    [Display(Name = "Blocked")]
    public bool IsBlocked { get; set; } = false;

    [Required]
    [Display(Name = "Scheduled Date Time")]
    public DateTime ScheduleTimeUtc { get; set; }

    [Required]
    [Display(Name = "Payment Period")]
    public PaymentPeriodType PaymentPeriod { get; set; }

    [ForeignKey("AccountNumber")]
    public virtual Account Account { get; set; }
    public virtual Payee Payee { get; set; }
}
