using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Users;

public class UserChangePassword
{
    public long UserId { get; set; }
    [Required, StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    [Compare("Password", ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
