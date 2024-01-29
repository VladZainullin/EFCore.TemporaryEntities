using System.Reflection;

namespace EFCore.TemporaryTables;

public sealed class TemporaryTableOptions
{
    internal TemporaryTableOptions()
    {
        
    }
    
    public ICollection<Assembly> Assemblies { get; } = new List<Assembly>(6);
}