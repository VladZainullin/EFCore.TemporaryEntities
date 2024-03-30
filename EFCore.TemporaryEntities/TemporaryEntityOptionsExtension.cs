using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryTables;

public sealed class TemporaryEntityOptionsExtension<TCreateOperation, TDropOperation, TCreateFromQueryableOperation> :
    IDbContextOptionsExtension
    where TCreateOperation : class, ICreateTemporaryEntityOperation
    where TDropOperation : class, IDropTemporaryEntityOperation
    where TCreateFromQueryableOperation : class, ICreateTemporaryEntityFromQueryableOperation
{
    public void ApplyServices(IServiceCollection services)
    {
        services.AddScoped<ITemporaryEntityConfigurator, TemporaryEntitiesConfigurator>();
        services.AddScoped<ITemporaryRelationalModelCreator, TemporaryRelationalModelCreator>();
        services.Decorate<IModelCustomizer, TemporaryModelCustomizer>();
        services.AddScoped<ICreateTemporaryEntityOperation, TCreateOperation>();
        services.AddScoped<IDropTemporaryEntityOperation, TDropOperation>();
        services.AddScoped<ICreateTemporaryEntityFromQueryableOperation, TCreateFromQueryableOperation>();
    }

    public void Validate(IDbContextOptions options)
    {
    }

    public DbContextOptionsExtensionInfo Info => new ExtensionInfo(this);

    private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
    {
        private string? _logFragment;

        public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override bool IsDatabaseProvider => default;
        public override string LogFragment => _logFragment ??= "using temporary tables";

        public override int GetServiceProviderHashCode()
        {
            return default;
        }

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
        {
            return other is ExtensionInfo;
        }

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
        }
    }
}