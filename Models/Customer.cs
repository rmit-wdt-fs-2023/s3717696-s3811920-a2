using System.ComponentModel.DataAnnotations;

namespace MCBA_Web.Models;

public class Customer 
{

    [StringLength(4, ErrorMessage = "Must be 4 digits", MinimumLength = 4)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
    [RegularExpression("^^[A-Za-z-' ]*$", ErrorMessage = "Name must be alpha")]
    public string Name { get; set; }

    [Required(ErrorMessage = "TFN is required")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "TFN must be 11 digits")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "TFN must be numeric")]
    public string TFN { get; set; }

    [StringLength(100, ErrorMessage = "Address must be less than 100 characters")]
    public string Address { get; set; }

    [StringLength(50, ErrorMessage = "City must be less than 50 characters")]
    public string City { get; set; }

    [EnumDataType(typeof(StateType), ErrorMessage = "Invalid State Type")]
    public StateType? State { get; set; }

    [StringLength(10, ErrorMessage = "Postcode must be less than 10 characters")]
    public string Postcode { get; set; }

    [StringLength(10, ErrorMessage = "Mobile must be 10 digits", MinimumLength = 10)]
    [RegularExpression("^04[0-9]*$", ErrorMessage = "Mobile must start with 04 and only contain numeric values")]
    public string Mobile { get; set; }
}
