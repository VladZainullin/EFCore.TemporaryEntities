namespace EFCore.TemporaryEntities.Sample;

public sealed class People
{
    public Guid Id { get; init; }

    public Identification Identification { get; init; } = default!;
    
    public Family Family { get; init; } = default!;

    public Work Work { get; init; } = default!;

    public Address Address { get; init; } = default!;
}

public sealed class Identification
{
    public string Name { get; init; } = default!;

    public string Surname { get; init; } = default!;

    public Gender Gender { get; init; }
}

public sealed class Family
{
    public int QuantityOfChildren { get; init; }

    public bool HasPartner { get; init; }

    public bool HasChildren => QuantityOfChildren != default;
}

public sealed class Work
{
    public decimal Salary { get; init; }

    public Address Address { get; init; } = default!;
}

public sealed class Address
{
    public string Country { get; init; } = default!;

    public string Region { get; init; } = default!;

    public string City { get; init; } = default!;

    public string Street { get; init; } = default!;

    public string House { get; init; } = default!;
}