using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace MCBA_Admin.Services;

/*
 * This class is specifically for admin functionality. All requests 
 * are performed by an API call to the ADMIN-API project.
 *
 * Implements IAdminService
 */
public class AdminService : IAdminService
{
    private readonly HttpClient _httpClient;
    public AdminService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7016/"); // ADMIN-API URI
    }

    // Returns all Logins from api call
    public async Task<List<Login>> GetAllCustomerLoginsAsync()
    {
        var response = await _httpClient.GetAsync("api/v1/customerlogin");
        if (response.IsSuccessStatusCode)
        {
            var logins = await response.Content.ReadFromJsonAsync<List<Login>>();
            return logins;
        }
        else
        {
            // Handle error cases
            throw new Exception("Failed to retrieve logins.");
        }
    }

    // Returns true if bill was un-blocked successfully. Requires the new BillPay model as parameter
    public async Task<bool> UnBlockBillPayAsync(BillPay bill)
    {

        bill.IsBlocked = false;

        // Serialize the customer object to a JSON string
        var json = JsonConvert.SerializeObject(bill);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("api/v1/BillPay/" + bill.BillPayID, content);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception("Failed to retrieve billpay.");
        }
    }

    // Returns true if bill was blocked successfully. Requires the new BillPay model as parameter
    public async Task<bool> BlockBillPayAsync(BillPay bill)
    {

        bill.IsBlocked = true;

        // Serialize the customer object to a JSON string
        var json = JsonConvert.SerializeObject(bill);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("api/v1/BillPay/" + bill.BillPayID, content);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception("Failed to retrieve billpay.");
        }
    }

    // Returns true if the passed Customer was updated successfully.
    public async Task<bool> UpdateCustomerAsync(int customerId, Customer customer)
    {
        // Serialize the customer object to a JSON string
        var json = JsonConvert.SerializeObject(customer);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("api/v1/customer/" + customerId, content);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception("Failed to retrieve customers.");
        }
    }

    // Updates Customer Address by passed customerId and new Address Model
    public async Task<bool> UpdateAddressAsync(int customerId, Address address)
    {
        // Set foreign key
        address.CustomerID = customerId;
        
        // Serialize the customer object to a JSON string
        var json = JsonConvert.SerializeObject(address);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine("--------------------------");
        Console.WriteLine(json);
        var response = await _httpClient.PutAsync("api/v1/address/" + address.AddressID, content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    // Creates Address for a customer by customerId and new Address model
    public async Task<bool> CreateAddressAsync(int customerId, Address address)
    {

        // Set foreign key
        address.CustomerID = customerId;

        // Serialize the customer object to a JSON string
        var json = JsonConvert.SerializeObject(address);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/v1/address/", content);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception("Failed to update address.");
        }
    }

    // Returns all Customers
    public async Task<List<Customer>> GetAllCutomersAsync()
    {
        var response = await _httpClient.GetAsync("api/v1/customer");
        if (response.IsSuccessStatusCode)
        {
            var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
            return customers;
        }
        else
        {
            // Handle error cases
            throw new Exception("Failed to retrieve customers.");
        }
    }

    // Returns a Customer from passed customer id
    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/v1/customer/{id}");

        if (response.IsSuccessStatusCode)
        {
            var customer = await response.Content.ReadFromJsonAsync<Customer>();
            return customer;
        }
        else
        {
            // Handle error cases
            throw new Exception("Failed to retrieve customer");
        }
    }

    // Returns BillPay by the passed billpay id
    public async Task<BillPay> GetBillPayByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/v1/billpay/{id}");
        if (response.IsSuccessStatusCode)
        {
            var bill = await response.Content.ReadFromJsonAsync<BillPay>();
            return bill;
        }
        else
        {
            // Handle error cases
            throw new Exception("Failed to retrieve customers.");
        }
    }

    // Returns all BillPays 
    public async Task<List<BillPay>> GetAllBillPayAsync()
    {
        var response = await _httpClient.GetAsync("api/v1/billpay");
        if (response.IsSuccessStatusCode)
        {
            var bills = await response.Content.ReadFromJsonAsync<List<BillPay>>();
            return bills;
        }
        else
        {
            // Handle error cases
            throw new Exception("Failed to retrieve customers.");
        }
    }

    // Returns all BillPays for a specific customer by the passed id
    public async Task<List<BillPay>> GetBillsByCustomerIdAsync(int id)
    {
        var allBills = await GetAllBillPayAsync();
        var accounts = await GetAccountsByCustomerIdAsync(id);

        var customerBills = new List<BillPay>();

        foreach (var account in accounts)
        {
            foreach (var bill in allBills)
            {
                if (account.AccountNumber == bill.AccountNumber)
                    customerBills.Add(bill);
            }
        }

        return customerBills;
    }

    // Returns all Accounts for a customer by the customer id
    public async Task<List<Account>> GetAccountsByCustomerIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/v1/account/customer/{id}");
        if (response.IsSuccessStatusCode)
        {
            var accounts = await response.Content.ReadFromJsonAsync<List<Account>>();
            return accounts;
        }
        else
        {
            // Handle error cases
            throw new Exception("Failed to retrieve customers.");
        }
    }

    // Returns a Login by the passed loginId
    public async Task<Login> GetCustomerLoginByIdAsync(int loginId)
    {
        var response = await _httpClient.GetAsync($"api/v1/customerlogin/{loginId}");
        if (response.IsSuccessStatusCode)
        {
            var login = await response.Content.ReadFromJsonAsync<Login>();
            return login;
        }
        else
        {
            // Handle error cases
            throw new Exception("Failed to retrieve login.");
        }
    }

    // Locks a Login by the passed Login model.
    public async Task<bool> LockCustomerAsync(Login login)
    {
        login.IsLocked = true;

        // Serialize the customer object to a JSON string
        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("api/v1/customerlogin/" + login.LoginID, content);
        
        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    // Un-Locks a Login by the passed Login model.
    public async Task<bool> UnLockCustomerAsync(Login login)
    {
        login.IsLocked = false;

        // Serialize the customer object to a JSON string
        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync("api/v1/customerlogin/" + login.LoginID, content);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception("Failed to update.");
        }
    }
}