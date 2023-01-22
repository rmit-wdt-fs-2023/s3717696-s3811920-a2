using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MCBA_Admin.Models;

public class Address
{
    [Key]
    public int AddressID { get; set; }

<<<<<<< HEAD
<<<<<<< HEAD
    [StringLength(100, ErrorMessage = "Street must be less than 100 characters")]
=======
    [StringLength(100, ErrorMessage = "Address must be less than 100 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street must be alpha")]
>>>>>>> dev
=======
    [StringLength(100, ErrorMessage = "Address must be less than 100 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street must be alpha")]
>>>>>>> dev
    public string Street { get; set; }

    [StringLength(50, ErrorMessage = "City must be less than 50 characters")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City must be alpha")]
    public string City { get; set; }

    [RegularExpression(@"^\d{4}$", ErrorMessage = "Postcode must be 4 digits")]
    public string Postcode { get; set; }

    [EnumDataType(typeof(StateType), ErrorMessage = "Invalid State Type")]
    [Column(TypeName = "varchar(20)")]
    public StateType? State { get; set; }

    [ForeignKey("CustomerID")]
    [JsonIgnore]
    public Customer Customer { get; set; }
    public int CustomerID { get; set; }
}
