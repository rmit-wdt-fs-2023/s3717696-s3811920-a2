using MCBA_Admin.Data;
using MCBA_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Admin.API.Repositories;

public class CustomerLoginRepository : ICustomerLoginRepository
{
    private readonly MCBAContext _context;

    public CustomerLoginRepository(MCBAContext context)
    {
        _context = context;
    }

    public async Task UpdateLoginDetailsAsync(Login login)
    {
        _context.Login.Update(login);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Login>> GetAllLoginsAsync()
    {
        return await _context.Login.ToListAsync();
    }


    public async Task<Login> GetLoginByIdAsync(int loginId)
    {
        return await _context.Login.FindAsync(loginId);
    }
}


