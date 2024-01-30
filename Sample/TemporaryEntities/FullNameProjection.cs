using EFCore.TemporaryTables.Attributes;

namespace Sample;

[TemporaryTable]
public sealed class FullNameProjection
{
    public required int Id { get; set; }

    public required string FullName { get; set; }
}