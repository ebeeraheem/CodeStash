namespace CodeStash.Infrastructure.Seeder;

public class DefaultUserConfig
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public bool IsValid() =>
        !string.IsNullOrEmpty(FirstName) &&
        !string.IsNullOrEmpty(LastName) &&
        !string.IsNullOrEmpty(Email) &&
        !string.IsNullOrEmpty(UserName) &&
        !string.IsNullOrEmpty(Password);
}
