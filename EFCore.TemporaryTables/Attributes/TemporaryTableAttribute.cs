namespace EFCore.TemporaryTables.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TemporaryTableAttribute : Attribute
{
    public TemporaryTableAttribute()
    {
    }
}