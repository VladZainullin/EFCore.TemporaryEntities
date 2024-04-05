namespace EFCore.TemporaryEntities.Sample;

public sealed class People
{
    public required Guid Id { get; init; }

    public required Identification Identification { get; init; } = default!;
    
    public required Family Family { get; init; } = default!;

    public required Work Work { get; init; } = default!;

    public required Address Address { get; init; } = default!;
}

public sealed class Identification
{
    public required string Name { get; init; } = default!;

    public required string Surname { get; init; } = default!;

    public required Gender Gender { get; init; }
}

public sealed class Family
{
    public required int QuantityOfChildren { get; init; }

    public required bool HasPartner { get; init; }

    public  bool HasChildren => QuantityOfChildren != default;
}

public sealed class Work
{
    public required decimal Salary { get; init; }

    public required Address Address { get; init; } = default!;
}

public sealed class Address
{
    public required string Country { get; init; } = default!;

    public required string Region { get; init; } = default!;

    public required string City { get; init; } = default!;

    public required string Street { get; init; } = default!;

    public required string House { get; init; } = default!;
}

public enum Gender
{
    Male,
    Female
}