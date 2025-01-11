using System.ComponentModel.DataAnnotations;

namespace CodeStash.Application.Models;
public class ResetPasswordModel
{
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public required string NewPassword { get; set; }

    [Compare(nameof(NewPassword))]
    public required string ConfirmPassword { get; set; }
}
