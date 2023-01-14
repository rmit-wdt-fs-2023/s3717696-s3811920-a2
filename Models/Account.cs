using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Web.Models;

public class Account
{
    [Key]
    [Required]
    [StringLength(4, ErrorMessage = "Account number must be 4 digits", MinimumLength = 4)]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Account number must be numeric")]
    public int AccountNumber { get; set; }

    [Required(ErrorMessage = "Account type is required")]
    [EnumDataType(typeof(AccountType))]
    public AccountType AccountType { get; set; }

    [Required(ErrorMessage = "Customer ID is required")]
    public int CustomerID { get; set; }

    [ForeignKey("CustomerID")]
    public virtual Customer Customer { get; set; }
}