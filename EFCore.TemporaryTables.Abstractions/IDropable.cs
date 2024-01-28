namespace EFCore.TemporaryTables.Abstractions;

public interface IDropable
{
    Task DropAsync(CancellationToken cancellationToken = default);
}