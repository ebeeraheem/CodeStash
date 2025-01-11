using CodeStash.Application.Interfaces;

namespace CodeStash.Infrastructure.Persistence;
public class TransactionManager(ApplicationDbContext context) : ITransactionManager
{
    public async Task ExecuteInTransactionAsync(Func<Task> operation)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await operation();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var result = await operation();
            await transaction.CommitAsync();

            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
