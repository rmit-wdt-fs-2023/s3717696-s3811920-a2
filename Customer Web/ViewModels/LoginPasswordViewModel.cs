using System;
using System.ComponentModel.DataAnnotations;
using MCBA_Web.Models;

namespace MCBA_Web.ViewModels;

	public class LoginPasswordViewModel
{
    [Required]
    public int CustomerID { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
