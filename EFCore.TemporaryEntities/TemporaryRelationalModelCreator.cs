using EFCore.TemporaryTables.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace EFCore.TemporaryTables;

internal sealed class TemporaryRelationalModelCreator : ITemporaryRelationalModelCreator
{
    private readonly IConventionSetBuilder _conventionSetBuilder;
    private readonly ITemporaryEntityConfigurator _configureTemporaryEntity;
    private readonly IModelRuntimeInitializer _modelRuntimeInitializer;

    public TemporaryRelationalModelCreator(
        IConventionSetBuilder conventionSetBuilder,
        ITemporaryEntityConfigurator configureTemporaryEntity,
        IModelRuntimeInitializer modelRuntimeInitializer)
    {
        _conventionSetBuilder = conventionSetBuilder;
        _configureTemporaryEntity = configureTemporaryEntity;
        _modelRuntimeInitializer = modelRuntimeInitializer;
    }

    public IRelationalModel Create<TEntity>() where TEntity : class
    {
        var conventionSet = _conventionSetBuilder.CreateConventionSet();
        var modelBuilder = new ModelBuilder(conventionSet);

        _configureTemporaryEntity.Configure<TEntity>(modelBuilder);

        var model = modelBuilder.Model;
        
        var finalizeModel = model.FinalizeModel();

        _modelRuntimeInitializer.Initialize(finalizeModel);
        
        var relationalFinalizeModel = finalizeModel.GetRelationalModel();

        return relationalFinalizeModel;
    }
}