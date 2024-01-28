namespace EFCore.TemporaryTables.Abstractions;

public interface IExistable
{
    Task<bool> ExistsAsync(CancellationToken cancellationToken = default);
}