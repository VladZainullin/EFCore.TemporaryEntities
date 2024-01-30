namespace EFCore.TemporaryTables.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TemporaryTableAttribute : Attribute
{
    public TemporaryTableAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}