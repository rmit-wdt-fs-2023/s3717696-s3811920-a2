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
    public AccountType AccountType { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal Balance { get; set; }

    [Required(ErrorMessage = "Customer ID is required")]
    [ForeignKey("CustomerID")]
    public int CustomerID { get; set; }
    public virtual Customer Customer { get; set; }
}