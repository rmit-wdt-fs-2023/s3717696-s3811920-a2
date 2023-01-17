﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MCBA_Admin.Models;

public class Address
{
    [Key]
    public int AddressID { get; set; }

    [StringLength(100, ErrorMessage = "Address must be less than 100 characters")]
    public string Street { get; set; }

    [StringLength(50, ErrorMessage = "City must be less than 50 characters")]
    public string City { get; set; }

    [StringLength(10, ErrorMessage = "Postcode must be less than 10 characters")]
    public string Postcode { get; set; }

    [EnumDataType(typeof(StateType), ErrorMessage = "Invalid State Type")]
    [Column(TypeName = "varchar(20)")]
    public StateType? State { get; set; }

    [ForeignKey("CustomerID")]
    [JsonIgnore]
    public Customer Customer { get; set; }
    public int CustomerID { get; set; }
}
