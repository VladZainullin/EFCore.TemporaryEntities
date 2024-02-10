using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace EFCore.TemporaryTables.Services;

internal sealed class TemporaryModelBuilderFactory
{
    private readonly IConventionSetBuilder _conventionSetBuilder;
    private readonly IModelRuntimeInitializer _modelRuntimeInitializer;
    private readonly TemporaryTableConfigurator _temporaryTableConfigurator;

    public TemporaryModelBuilderFactory(
        IConventionSetBuilder conventionSetBuilder,
        IModelRuntimeInitializer modelRuntimeInitializer,
        TemporaryTableConfigurator temporaryTableConfigurator)
    {
        _conventionSetBuilder = conventionSetBuilder;
        _modelRuntimeInitializer = modelRuntimeInitializer;
        _temporaryTableConfigurator = temporaryTableConfigurator;
    }

    public IRelationalModel CreateRelationalModelForTemporaryEntity<TEntity>() where TEntity : class
    {
        var conventionSet = _conventionSetBuilder.CreateConventionSet();
        var modelBuilder = new ModelBuilder(conventionSet);

        _temporaryTableConfigurator.Configure<TEntity>(modelBuilder);

        var model = modelBuilder.Model;
        var finalizeModel = model.FinalizeModel();

        _modelRuntimeInitializer.Initialize(finalizeModel);

        var relationalFinalizeModel = finalizeModel.GetRelationalModel();

        return relationalFinalizeModel;
    }
}