using EFCore.TemporaryTables.Attributes;

namespace Sample;

[TemporaryTable]
public sealed class Projection
{
    public required int Id { get; set; }

    public required string FullName { get; set; }
}