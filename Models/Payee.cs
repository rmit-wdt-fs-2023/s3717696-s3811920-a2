using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCBA_Web.Models;

public class Payee
{
    [Required]
    [Key]
    public int PayeeID { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
    public string Name { get; set; }

    [Required]
    [RegularExpression("^\\d{14}$", ErrorMessage = "Phone number must be 14 digits")]
    public int Phone { get; set; }

    [Required]
    public int AddressID { get; set; }

    [ForeignKey("AddressID")]
    public virtual Address Address { get; set; }
}

