using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public interface IAdminService
{
    Task<List<Customer>> GetAllCutomersAsync();
}