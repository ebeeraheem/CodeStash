namespace CodeStash.Application.Interfaces;
public interface ITransactionManager
{
    Task ExecuteInTransactionAsync(Func<Task> operation);
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);
}
