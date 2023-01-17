using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MCBA_Admin.Models;

namespace MCBA_Admin.Filters;

public class AuthorizeAdminAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {

        var adminUsername = context.HttpContext.Session.GetString(nameof(AdminLogin.Username));

        if(adminUsername == null || adminUsername == "")
            context.Result = new RedirectToActionResult("Index", "Login", null);
    }
}
