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
    public string Name { get; set; }

    [Required]
    [RegularExpression("^\\(\\d{2}\\) \\d{4} \\d{4}$", ErrorMessage = "Phone number must be of the format '(04) 3333 3333'")]
    public string Phone { get; set; }

    [Required]
    [StringLength(40)]
    public string Address { get; set; }

    [Required]
    public StateType State { get; set; }

    [Required]
    [StringLength(4)]
    public string Postcode { get; set; }

    [Required]
    [StringLength(14)]
    public string City { get; set; }
}

