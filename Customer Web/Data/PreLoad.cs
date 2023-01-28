using MCBA_Web.DTO;
using MCBA_Web.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

public sealed class PreLoad
{
    // Common
    private const string _dataURI = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/"; // URL to the data
    private readonly HttpClient _client;
    private string _jsonData;

    // DTO Lists
    private List<CustomerDTO> _customersDTO;
    private List<AccountDTO> _accountsDTO;
    private List<TransactionDTO> _transactionsDTO;
    private List<LoginDTO> _loginsDTO;
    private List<AddressDTO> _addressesDTO;

    // Model Lists
    private List<Customer> _customers;
    private List<Account> _accounts;
    private List<Transaction> _transactions;
    private List<Login> _logins;
    private List<Address> _addresses;


    public PreLoad()
    {
        _client = new();


        _accountsDTO = new();
        _transactionsDTO = new();
        _loginsDTO = new();
        _addressesDTO = new();

        _customers = new();
        _accounts = new();
        _transactions = new();
        _logins = new();
        _addresses = new();
    }

    // Main runner
    public void Run()
    {

        if (!DownloadData()) // Checks if data was downloaded correctly 
            throw new Exception("Error occurred while trying to download required data");

        if (!DeserializeData()) // Deserializes data into its respective objects => _customers
            throw new Exception("Error occurred while deserializing data");

        PresetData(); // Adds required logic code to objects

        ConvertDTOToModel(); // Converts DTOs to Models
    }
    private int FindCustomerIDByAddress(string Street, string City, string PostCode)
    {
        foreach (var customer in _customersDTO)
        {
            if (Street == customer.Address && City == customer.City && PostCode == customer.PostCode)
                return customer.CustomerID;
        }
        
        throw new Exception("No customerID found from the address");
    }

    private AccountType ToAccountType(char accountType)
    {
        if (accountType == 'C')
            return AccountType.Checking;

        if (accountType == 'S')
            return AccountType.Savings;

        throw new Exception("No matching AccountType");
    }
    
    // Converts DTO to Model
    private void ConvertDTOToModel()
    {
        // CustomerDTO -> Customer
        foreach (var customer in _customersDTO)
        {
            Customer c = new Customer
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name
            };
            _customers.Add(c);
        }

        // AccountDTO -> Account
        foreach (var account in _accountsDTO)
        {
            Account a = new Account
            {
                CustomerID = account.CustomerID,
                AccountNumber= account.AccountNumber,
                AccountType = ToAccountType(account.AccountType),
                Balance= account.Balance
            };
            _accounts.Add(a);
        }

        // AddressDTO -> Address
        foreach (var address in _addressesDTO)
        {
            Address a = new Address
            {
                Street = address.Street,
                City = address.City,
                Postcode = address.PostCode,
                CustomerID = FindCustomerIDByAddress(address.Street, address.City, address.PostCode) // Should have a better solution
            };
            _addresses.Add(a);
        }

        // TransactionDTO -> Transaction
        foreach (var transaction in _transactionsDTO)
        {
            Transaction t = new Transaction
            {
                AccountNumber = transaction.AccountNumber,
                Comment = transaction.Comment,
                TransactionID = transaction.TransactionID,
                TransactionTimeUtc= transaction.TransactionTimeUtc,
                Amount= transaction.Amount,
                DestinationAccountNumber= null,
                TransactionType = ToTransactionType(transaction.TransactionType)
            };
            _transactions.Add(t);
        }

        // LoginDTO -> Login
        foreach (var login in _loginsDTO)
        {
            Login l = new Login
            {
                CustomerID = login.CustomerID,
                LoginID = login.LoginID,
                PasswordHash = login.PasswordHash,
            };
            _logins.Add(l);
        }
    }
    private TransactionType ToTransactionType(char transactionType)
    {
        if (transactionType == 'S')
            return TransactionType.ServiceCharge;

        if (transactionType == 'D')
            return TransactionType.Deposit;

        if (transactionType == 'W')
            return TransactionType.Withdraw;

        if (transactionType == 'T')
            return TransactionType.Transfer;

        if (transactionType == 'B')
            return TransactionType.BillPay;

        throw new Exception("No matching TransactionType");
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
        _customersDTO = JsonConvert.DeserializeObject<List<CustomerDTO>>(_jsonData, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        });

        if (_customersDTO == null)
            return false;

        return true;
    }

    // Logic code to prevent invalid data
    private void PresetData()
    {
        foreach (var customer in _customersDTO)
        {
            foreach (var account in customer.Accounts)
            {
                account.Balance = AccountBalance(account.Transactions);
                _accountsDTO.Add(account);
                foreach (var transaction in account.Transactions)
                {
                    transaction.TransactionType = 'D';
                    transaction.AccountNumber = account.AccountNumber;
                    _transactionsDTO.Add(transaction);
                }
            }
            customer.Login.CustomerID = customer.CustomerID;

            if (customer.Address != null || customer.City != null || customer.PostCode != null)
            {
                _addressesDTO.Add(new AddressDTO
                {
                    Street = customer.Address,
                    PostCode = customer.PostCode,
                    City = customer.City,
                });
            }

            _loginsDTO.Add(customer.Login);
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


    public Payee[] InsertPayeeDataForInMemeoryDatabase()
    {
        Payee[] payees =
            {
                new Payee
                {
                    PayeeID = 1,
                    Name = "Telstra",
                    Phone = "0300300300",
                    Street = "ABC Street",
                    State = StateType.VIC,
                    Postcode = "0000",
                    City = "Melbourne"
                },

                new Payee
                {
                    PayeeID = 2,
                    Name = "Optus",
                    Phone = "01001001000",
                    Street = "XYZ Street",
                    State = StateType.VIC,
                    Postcode = "0000",
                    City = "Melbourne"
                },
            };

        return payees;
    }


    public BillPay[] InsertBillPayDataForInMemeoryDatabase()
    {
        BillPay[] bills =
        {
            new BillPay
            {
                BillPayID = 1,
                AccountNumber = 4100,
                PayeeID = 1,
                Amount = 100,
                IsBlocked = false,
                ScheduleTimeUtc = DateTime.UtcNow,
                PaymentPeriod = PaymentPeriodType.OneTime,
            },
            new BillPay
            {
                BillPayID = 2,
                AccountNumber = 4100,
                PayeeID = 2,
                Amount = 200,
                IsBlocked = false,
                ScheduleTimeUtc = DateTime.UtcNow,
                PaymentPeriod = PaymentPeriodType.OneTime,
            },
            new BillPay
            {
                BillPayID = 3,
                AccountNumber = 4300,
                PayeeID = 2,
                Amount = 100,
                IsBlocked = false,
                ScheduleTimeUtc = DateTime.UtcNow,
                PaymentPeriod = PaymentPeriodType.Monthly,
            }
        };

        return bills;
    }
    

    public List<Account> GetAccounts()
    {
        return _accounts;
    }

    public List<Address> GetAddresses()
    {
        return _addresses;
    }

    public List<Customer> GetCustomers()
    {
        return _customers;
    }

    public List<Transaction> GetTransactions()
    {
        return _transactions;
    }

    public List<Login> GetLogins()
    {
        return _logins;
    }

    public Payee[] GetPayees()
    {
        return InsertPayeeDataForInMemeoryDatabase();
    }

    public BillPay[] GetBillPays()
    {
        return InsertBillPayDataForInMemeoryDatabase();
    }
}