using EFCore.TemporaryTables.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace EFCore.TemporaryTables.Services;

internal sealed class TemporaryModelBuilderFactory : ITemporaryModelBuilderFactory
{
    private readonly IConventionSetBuilder _conventionSetBuilder;
    private readonly IModelRuntimeInitializer _modelRuntimeInitializer;
    private readonly TemporaryTableConfiguration _temporaryTableConfiguration;

    public TemporaryModelBuilderFactory(
        IConventionSetBuilder conventionSetBuilder,
        IModelRuntimeInitializer modelRuntimeInitializer,
        TemporaryTableConfiguration temporaryTableConfiguration)
    {
        _conventionSetBuilder = conventionSetBuilder;
        _modelRuntimeInitializer = modelRuntimeInitializer;
        _temporaryTableConfiguration = temporaryTableConfiguration;
    }

    public IRelationalModel CreateRelationalModelForTemporaryEntity<TEntity>() where TEntity : class
    {
        var configureTemporaryEntity = _temporaryTableConfiguration.Get<TEntity>();
        if (ReferenceEquals(configureTemporaryEntity, default)) throw new InvalidOperationException();

        var conventionSet = _conventionSetBuilder.CreateConventionSet();

        var modelBuilder = new ModelBuilder(conventionSet);

        configureTemporaryEntity(modelBuilder.Entity<TEntity>());

        var model = modelBuilder.Model;
        var finalizeModel = model.FinalizeModel();

        _modelRuntimeInitializer.Initialize(finalizeModel);

        var relationalFinalizeModel = finalizeModel.GetRelationalModel();

        return relationalFinalizeModel;
    }
}