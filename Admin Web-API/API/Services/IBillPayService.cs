using MCBA_Admin.Models;

namespace MCBA_Admin.Services;

public interface IBillPayService
{
    Task<BillPay> GetByIdAsync(int id);
    Task<List<BillPay>> GetAllAsync();

    void Update(int id, BillPay billPay);
}
