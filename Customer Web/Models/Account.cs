using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Web.Models;

public class Account
{
    [Key]
    [Required]
    [RegularExpression("^\\d{4}", ErrorMessage = "Account number must be 4 digits")]
    public int AccountNumber { get; set; }

    [Required(ErrorMessage = "Account type is required")]
    [EnumDataType(typeof(AccountType))]
    [Column(TypeName = "varchar(20)")]
    public AccountType AccountType { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal Balance { get; set; }

    public virtual List<Transaction> Transactions { get; set; } = new List<Transaction>();

    [Required(ErrorMessage = "Customer ID is required")]
    [ForeignKey("CustomerID")]
    public int CustomerID { get; set; }
    public virtual Customer Customer { get; set; }
}