using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Web.Models;
public class Customer 
{
    public int CustomerID { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name must be less than 50 characters")]
    [RegularExpression("^^[A-Za-z-' ]*$", ErrorMessage = "Name must be alpha")]
    public string Name { get; set; }

    [RegularExpression("^\\d{3} \\d{3} \\d{3}$", ErrorMessage = "TFN must be 9 digits (Eg. 333 333 333)")]
    public string? TFN { get; set; }

    [RegularExpression("^04\\d{2} \\d{3} \\d{3}$", ErrorMessage = "Mobile must start wih 04 and be of the format '0433 333 333' ")]
    public string? Mobile { get; set; }

    public byte[] ProfilePicture { get; set; }

    [NotMapped]
    public IFormFile ImageUpload { get; set; }

    public string ProfilePictureContentType { get; set; }
    public bool HasDefaultProfilePicture { get; set; } = true;

    public Address Address { get; set; }

    [NotMapped]
    public Login Login { get; set; }

    public List<Account> Accounts { get; set; }
}
