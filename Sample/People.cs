namespace Sample;

public sealed class People
{
    public required Guid Id { get; init; }

    public required Identification Identification { get; init; }

    public required Family Family { get; init; }

    public required Work Work { get; init; }

    public required Address Address { get; init; }
}

public sealed class Identification
{
    public required string Name { get; init; }

    public required string Surname { get; init; }

    public required DateTime DateOfBirth { get; init; }

    public required Gender Gender { get; init; }
}

public sealed class Family
{
    public required int QuantityOfChildren { get; init; }

    public required bool HasPartner { get; init; }

    public bool HasChildren => QuantityOfChildren != default;
}

public sealed class Work
{
    public required decimal Salary { get; init; }

    public required Address Address { get; init; }
}

public sealed class Address
{
    public required string Country { get; init; }

    public required string Region { get; init; }

    public required string City { get; init; }

    public required string Street { get; init; }

    public required string House { get; init; }
}