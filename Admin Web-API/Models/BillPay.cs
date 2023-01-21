using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCBA_Admin.Models;

public class BillPay
{
    [Required]
    [Key]
    public int BillPayID { get; set; }

    public int AccountNumber { get; set; }

    [ForeignKey("PayeeID")]
    public int PayeeID { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be positive")]
    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    public bool IsBlocked { get; set; } = false;

    [Required]
    public DateTime ScheduleTimeUtc { get; set; }

    [Required]
    public PaymentPeriodType PaymentPeriod { get; set; }

    [ForeignKey("AccountNumber")]
    public virtual Account Account { get; set; }
    public virtual Payee Payee { get; set; }
}
