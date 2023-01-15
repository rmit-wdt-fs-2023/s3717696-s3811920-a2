namespace MCBA_Web.DTO;

public class TransactionDTO
{
    public int TransactionID { get; set; }
    public char TransactionType { get; set; }
    public int AccountNumber { get; set; }
    public int DestinationAccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string Comment { get; set; }
    public DateTime TransactionTimeUtc { get; set; }

    public Dictionary<string, object> ToDictionary()
    {

        return new Dictionary<string, object>
        {
            { "TransactionType", TransactionType },
            { "AccountNumber", AccountNumber },
            { "DestinationAccountNumber", DBNull.Value }, // Hard coded to DBNULL.Value for transfer
            { "Amount", Amount },
            { "Comment", Comment },
            { "TransactionTimeUtc", TransactionTimeUtc}
        };
    }
}

