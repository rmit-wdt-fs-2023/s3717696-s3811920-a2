namespace MCBA.DTO;

public class CustomerDTO
{
    public int CustomerID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
    public List<AccountDTO> Accounts { get; set; }
    public LoginDTO Login { get; set; }


    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "CustomerID", CustomerID },
            { "Name", Name },
            { "Address", Address },
            { "City", City },
            { "Postcode", Postcode },
        };
    }
}

