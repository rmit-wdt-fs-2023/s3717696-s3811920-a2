using MCBA_Admin.Models;

namespace MCBA_Admin.API.Repositories;

public interface IBillPayRepository
{
    Task<BillPay> GetByIdAsync(int id);
    Task<List<BillPay>> GetAllAsync();

    void Update(BillPay billPay);
}
