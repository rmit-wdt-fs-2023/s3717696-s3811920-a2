using System.ComponentModel.DataAnnotations;

namespace MCBA_Web.Models;

public class Address
{
    public int AddressID { get; set; }

    [StringLength(100, ErrorMessage = "Address must be less than 100 characters")]
    public string Street { get; set; }

    [StringLength(50, ErrorMessage = "City must be less than 50 characters")]
    public string City { get; set; }

    [StringLength(10, ErrorMessage = "Postcode must be less than 10 characters")]
    public string Postcode { get; set; }

    [EnumDataType(typeof(StateType), ErrorMessage = "Invalid State Type")]
    public StateType? State { get; set; }
}
