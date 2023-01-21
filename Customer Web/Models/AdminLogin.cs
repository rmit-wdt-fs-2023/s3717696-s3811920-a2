using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Web.Models;
public class AdminLogin 
{

    [Key]
    [Required]
    public string Username { get; set; }

    public string Password { get; set; }

    public AdminLogin(string username, string password)
    {
        Username = username;
        Password = password;
    }

}
