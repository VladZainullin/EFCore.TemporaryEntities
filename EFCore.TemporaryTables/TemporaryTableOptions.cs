using System.Reflection;

namespace EFCore.TemporaryTables;

public sealed class TemporaryTableOptions
{
    public ICollection<Assembly> Assemblies { get; init; } = new List<Assembly>(6);
}