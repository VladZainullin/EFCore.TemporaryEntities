using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryTables;

public sealed class TemporaryTableOptionsExtension<TCreateOperation, TDropOperation, TCreateFromQueryableOperation> :
    IDbContextOptionsExtension
    where TCreateOperation : class, ICreateTemporaryTableOperation
    where TDropOperation : class, IDropTemporaryTableOperation
    where TCreateFromQueryableOperation : class, ICreateTemporaryTableFromQueryableOperation
{
    public void ApplyServices(IServiceCollection services)
    {
        services.AddScoped<TemporaryTablesConfigurator>();
        services.AddScoped<IAddTemporaryTableConfiguration>(s => s.GetRequiredService<TemporaryTablesConfigurator>());
        services.AddScoped<IConfigureTemporaryTable>(s => s.GetRequiredService<TemporaryTablesConfigurator>());
        services.AddScoped<ITemporaryRelationalModelCreator, TemporaryRelationalModelCreator>();
        services.Decorate<IModelCustomizer, TemporaryModelCustomizer>();
        services.AddScoped<ICreateTemporaryTableOperation, TCreateOperation>();
        services.AddScoped<IDropTemporaryTableOperation, TDropOperation>();
        services.AddScoped<ICreateTemporaryTableFromQueryableOperation, TCreateFromQueryableOperation>();
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