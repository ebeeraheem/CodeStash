using System.ComponentModel.DataAnnotations;

namespace CodeStash.Application.Models;
public class RegisterModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Length(minimumLength: 3, maximumLength: 50)]
    public required string UserName { get; set; }
}
