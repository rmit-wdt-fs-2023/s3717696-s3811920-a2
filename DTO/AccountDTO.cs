namespace MCBA.DTO;
public class AccountDTO
{
    public int AccountNumber { get; set; }
    public char AccountType { get; set; }
    public decimal Balance { get; set; }
    public int CustomerID{ get; set; }
    public List<TransactionDTO> Transactions { get; set; }
    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "AccountNumber", AccountNumber },
            { "AccountType", AccountType },
            { "Balance", Balance },
            { "CustomerID", CustomerID }
        };
    }

}
