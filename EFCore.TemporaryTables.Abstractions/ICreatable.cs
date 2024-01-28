namespace EFCore.TemporaryTables.Abstractions;

public interface ICreatable
{
    Task CreateAsync(CancellationToken cancellationToken = default);
}