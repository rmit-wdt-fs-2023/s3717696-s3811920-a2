using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MCBA_Admin.Models;

public class Login 
{

    [Key]
    [Required]
    [RegularExpression("^\\d{8}$", ErrorMessage = "LoginID must be 8 digits")]
    public int LoginID { get; set; }

    [JsonIgnore]
    public virtual Customer Customer { get; set; }
    public int CustomerID { get; set; }

    [Required]
    public string PasswordHash { get; set; }
}
