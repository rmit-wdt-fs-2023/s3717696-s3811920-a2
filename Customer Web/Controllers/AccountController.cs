using Microsoft.AspNetCore.Mvc;
using MCBA_Web.Models;
using MCBA_Web.ViewModels;
using MCBA_Web.Utilities;
using MCBA_Web.Services;
using X.PagedList;

namespace MCBA_Web.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
    }
}
