using System.ComponentModel.DataAnnotations;

namespace CodeStash.Application.Models;
public class RegisterModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Length(minimumLength: 3, maximumLength: 50, ErrorMessage = "Username must be between 3 and 50 characters long")]
    public required string UserName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
