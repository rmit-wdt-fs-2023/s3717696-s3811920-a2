using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Web.Models;

public class Login 
{

    [Key]
    [Required]
    [RegularExpression("^\\d{8}$", ErrorMessage = "LoginID must be 8 digits")]
    public int LoginID { get; set; }

    public int CustomerID { get; set; }
    public virtual Customer Customer { get; set; }

    [Required]
    public string PasswordHash { get; set; }
}
