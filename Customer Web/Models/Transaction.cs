using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Web.Models;

public class Transaction
{
    [Required]
    [Display(Name = "Transaction ID")]
    public int TransactionID { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    [Display(Name = "Transaction Type")]
    public TransactionType TransactionType { get; set; }

    [ForeignKey("Account")]
    [Display(Name = "Account Number")]
    public int AccountNumber { get; set; }
    public virtual Account Account { get; set; }

    [ForeignKey("DestinationAccount")]
    [Display(Name = "Destination Account Number")]
    public int? DestinationAccountNumber { get; set; }
    public virtual Account DestinationAccount { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be positive")]
    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal Amount { get; set; }

    [StringLength(30, ErrorMessage = "Comment must be less than 30 characters")]
    public string Comment { get; set; }

    [Required]
    [Display(Name = "Transaction DateTime")]
    public DateTime TransactionTimeUtc { get; set; }

}
