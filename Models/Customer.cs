using MCBA_Web.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MCBA_Web.Models;

public class Customer 
{

    [Range(0000, 9999, ErrorMessage = "ID must be 4 digits")]
    public int CustomerID { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
    [RegularExpression("^^[A-Za-z-' ]*$", ErrorMessage = "Name must be alpha")]
    public string Name { get; set; }

    [Range(1000000000, 9999999999, ErrorMessage = "TFN must be 11 digits")]
    public int? TFN { get; set; }

    [Range(1000000000, 9999999999, ErrorMessage = "Mobile must be 10 digits")]
    [RegularExpression("^04[0-9]*$", ErrorMessage = "Mobile must start with 04 and only contain numeric values")]
    public int? Mobile { get; set; }

    [Required]
    [ForeignKey("AddressID")]
    public int AddressID { get; set; }
    public virtual Address Address { get; set; }
}
