using Newtonsoft.Json;
using MCBA.DTO;

namespace MCBA.Feature;

public sealed class PreLoad
{
    private const string _dataURI = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/"; // URL to the data
    private readonly HttpClient _client;
    private string _jsonData;
    private List<CustomerDTO> _customers;
    private List<AccountDTO> _accounts;
    private List<TransactionDTO> _transactions;
    private List<LoginDTO> _logins;

    public PreLoad()
    {
        _database = new();
        _client = new();
        _accounts = new();
        _transactions = new();
        _logins = new();
    }

    // Main runner
    public void Run()
    {

        if (!DownloadData()) // Checks if data was downloaded correctly 
            throw new Exception("Error occurred while trying to download required data");

        if (!DeserializeData()) // Deserializes data into its respective objects => _customers
            throw new Exception("Error occurred while deserializing data");

        PresetData(); // Adds required logic code to objects

    }

    // Returns true if the data files are available and downloaded
    private bool DownloadData()
    {
        try
        {
            _jsonData = _client.GetStringAsync(_dataURI).Result;
        }
        catch
        {
            return false;
        }

        return true;
    }

    // Deserializes data into DTOs
    private bool DeserializeData()
    {
        _customers = JsonConvert.DeserializeObject<List<CustomerDTO>>(_jsonData, new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        });

        if (_customers == null)
            return false;

        return true;
    }

    // Logic code to prevent invalid data
    private void PresetData()
    {

        foreach (var customer in _customers)
        {
            foreach (var account in customer.Accounts)
            {
                account.Balance = AccountBalance(account.Transactions);
                _accounts.Add(account);
                foreach (var transaction in account.Transactions)
                {
                    transaction.TransactionType = 'D';
                    transaction.AccountNumber = account.AccountNumber;
                    _transactions.Add(transaction);
                }
            }
            customer.Login.CustomerID = customer.CustomerID;
            _logins.Add(customer.Login);
        }
    }

    // Utility method to get the account balance. Used by PresetData()
    private static decimal AccountBalance(List<TransactionDTO> transactions)
    {
        decimal total = 0;

        foreach (var transaction in transactions)
        {
            total += transaction.Amount;
        }

        return total;
    }


}