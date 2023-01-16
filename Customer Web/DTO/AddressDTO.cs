
namespace MCBA_Web.DTO;

public class AddressDTO
{
    public string Street { get; set; }

    public string City { get; set; }

    public string PostCode { get; set; }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "Street", Street },
            { "City", City },
            { "PostCode", PostCode }
        };
    }
}
