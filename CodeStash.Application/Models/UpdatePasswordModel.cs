namespace CodeStash.Application.Models;

public class UpdatePasswordModel
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}
