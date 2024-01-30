using EFCore.TemporaryTables.Attributes;

namespace Sample.TemporaryEntities;

[TemporaryTable]
public sealed class AgeProjection
{
    public required int Id { get; init; }

    public required int Age { get; init; }
}