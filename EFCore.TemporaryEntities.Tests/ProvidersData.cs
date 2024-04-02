using System.Collections;
using EFCore.TemporaryEntities.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EFCore.TemporaryEntities.Tests;

internal sealed class ProvidersData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new DbContextOptionsBuilder().UseSqlite(options =>
            {
                options.UseTemporaryEntities();
            }),
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}