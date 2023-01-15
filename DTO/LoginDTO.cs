namespace MCBA_Web.DTO;
public class LoginDTO
{
    public int LoginID { get; set; }
    public int CustomerID { get; set; }
    public string PasswordHash { get; set; }
    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "LoginID", LoginID },
            { "CustomerID", CustomerID },
            { "PasswordHash", PasswordHash }
        };
    }
}
