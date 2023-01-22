using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCBA_Admin.Models;

public class Payee
{
    [Required]
    [Key]
    public int PayeeID { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must be in alpha")]
    public string Name { get; set; }

    [Required]
    [RegularExpression("^\\(\\d{2}\\) \\d{4} \\d{4}$", ErrorMessage = "Phone number must be of the format '(04) 3333 3333'")]
    public string Phone { get; set; }

    [Required]
<<<<<<< HEAD
    [StringLength(100, ErrorMessage = "Street must be less than 100 characters")]
=======
    [StringLength(40)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street must be in alpha")]
>>>>>>> dev
    public string Street { get; set; }

    [Required]
    [EnumDataType(typeof(StateType), ErrorMessage = "Invalid State Type")]
    [Column(TypeName = "varchar(20)")]
    public StateType State { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}$", ErrorMessage = "Postcode must be 4 digits")]
    public string Postcode { get; set; }

    [Required]
    [StringLength(14)]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City must be in alpha")]
    public string City { get; set; }
}

