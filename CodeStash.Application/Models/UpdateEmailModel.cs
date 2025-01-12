using System.ComponentModel.DataAnnotations;

namespace CodeStash.Application.Models;

public class UpdateEmailModel
{
    public required string UserId { get; set; }
    public required string Token { get; set; }

    [EmailAddress]
    public required string NewEmail { get; set; }
}
