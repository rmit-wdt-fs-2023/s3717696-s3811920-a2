using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MCBA_Web.Models;

public class Transaction
{
    [Required]
    public int TransactionID { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public TransactionType TransactionType { get; set; }

    [ForeignKey("Account")]
    [JsonIgnore]
    public int AccountNumber { get; set; }
    public virtual Account Account { get; set; }

    [ForeignKey("DestinationAccount")]
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
    public DateTime TransactionTimeUtc { get; set; }

}
