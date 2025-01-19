namespace CodeStash.Application.Models;
public class RateLimitModel
{
    public const string FixedLimit = "fixed";
    public int PermitLimit { get; set; } = 10;
    public int Window { get; set; } = 10;
}
