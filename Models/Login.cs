using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Web.Models;

public class Login 
{

    [Key]
    [Required]
    [RegularExpression("^\\d{8}$", ErrorMessage = "LoginID must be 8 digits")]
    public int LoginID { get; set; }

    [Required]
    [ForeignKey("CustomerID")]
    public int CustomerID { get; set; }

    [Required]
    [StringLength(64, ErrorMessage = "PasswordHash must be 64 characters long")]
    public string PasswordHash { get; set; }

    public virtual Customer Customer { get; set; }
}
