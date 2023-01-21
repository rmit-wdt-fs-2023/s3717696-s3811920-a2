using MCBA_Admin.Models;
using NuGet.Protocol.Plugins;

namespace MCBA_Admin.Services;

public interface IAdminService
{
    Task<List<Customer>> GetAllCutomersAsync();
    Task<Customer> GetCustomerByIdAsync(int id);
    Task<BillPay> GetBillPayByIdAsync(int id);
    Task<List<BillPay>> GetAllBillPayAsync();
    Task<List<Account>> GetAccountsByCustomerIdAsync(int id);
    Task<bool> UpdateCustomerAsync(int customerId, Customer customer);
    Task<bool> UpdateAddressAsync(int customerId, Address customer);
    Task<List<BillPay>> GetBillsByCustomerIdAsync(int id);
    Task<bool> BlockBillPayAsync(BillPay bill);
    Task<List<Login>> GetAllCustomerLoginsAsync();
    Task<bool> UnBlockBillPayAsync(BillPay bill);
    Task<bool> CreateAddressAsync(int customerId, Address address);
    Task<Login> GetCustomerLoginByIdAsync(int loginId);
    Task<bool> LockCustomerAsync(Login login);
    Task<bool> UnLockCustomerAsync(Login login);
}