using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MCBA_Web.Models;
public class Account
{
    [Key]
    [Required]
    [RegularExpression("^\\d{4}", ErrorMessage = "Account number must be 4 digits")]
    public int AccountNumber { get; set; }

    [Required(ErrorMessage = "Account type is required")]
    [EnumDataType(typeof(AccountType))]
    [Column(TypeName = "varchar(20)")]
    public AccountType AccountType { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal Balance { get; set; }

    public List<Transaction> Transactions { get; set; }

    [ForeignKey("CustomerID")]
    [JsonIgnore]
    public Customer Customer { get; set; }
    public int CustomerID { get; set; }
}