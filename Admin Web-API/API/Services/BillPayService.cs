using MCBA_Admin.API.Repositories;
using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.Services;
public class BillPayService : IBillPayService
{
    private readonly IBillPayRepository _billPayRepository;
    public BillPayService(IBillPayRepository billPayRepository)
    {
        _billPayRepository = billPayRepository;
    }

    public async Task<BillPay> GetByIdAsync(int id)
    {
        return await _billPayRepository.GetByIdAsync(id);
    }

    public async Task<List<BillPay>> GetAllAsync()
    {
        return await _billPayRepository.GetAllAsync();
    }

    public void Update(int id, BillPay billPay)
    {
        billPay.BillPayID = id;

        _billPayRepository.Update(billPay);
    }
}
