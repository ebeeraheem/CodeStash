using System.ComponentModel.DataAnnotations;

namespace CodeStash.Application.Models;

public class UpdatePasswordModel
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }

    [Compare(nameof(NewPassword))]
    public required string ConfirmPassword { get; set; }
}
