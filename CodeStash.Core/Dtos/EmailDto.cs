using System.ComponentModel.DataAnnotations;

namespace CodeStash.Core.Dtos;
public class EmailDto
{
    [EmailAddress]
    public required string Email { get; set; }
}
