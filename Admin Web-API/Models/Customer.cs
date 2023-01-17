using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MCBA_Admin.Models;

public class Customer 
{
    public int CustomerID { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
    [RegularExpression("^^[A-Za-z-' ]*$", ErrorMessage = "Name must be alpha")]
    public string Name { get; set; }

    [RegularExpression("^\\d{11}$", ErrorMessage = "TFN must be 11 digits")]
    public string? TFN { get; set; }

    [RegularExpression("^04[0-9]{8}$", ErrorMessage = "Mobile must start with 04 and be 10 digits long")]
    public string? Mobile { get; set; }

    [DataType(DataType.Upload)]
    public string? ProfilePicture { get; set; } = "~/img/CustomerProfile/default_profile_picture.png";

    public Address Address { get; set; }
  
    public Login Login { get; set; }

    public List<Account> Accounts { get; set; }
}
