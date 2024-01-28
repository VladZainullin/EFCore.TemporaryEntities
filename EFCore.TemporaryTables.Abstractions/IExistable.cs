namespace EFCore.TemporaryTables.Abstractions;

public interface IExistable
{
    Task ExistsAsync(CancellationToken cancellationToken = default);
}