namespace CodeStash.Core.Dtos;
public class UserProfileDto
{
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Role { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
