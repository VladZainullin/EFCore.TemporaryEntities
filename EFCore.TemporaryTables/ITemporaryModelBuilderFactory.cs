using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace EFCore.TemporaryTables;

public interface ITemporaryModelBuilderFactory
{
    IRelationalModel CreateRelationalModelForTemporaryEntity<TEntity>() where TEntity : class;
}

internal sealed class TemporaryModelBuilderFactory : ITemporaryModelBuilderFactory
{
    private readonly IConventionSetBuilder _conventionSetBuilder;
    private readonly IDesignTimeModel _designTimeModel;
    private readonly IModelRuntimeInitializer _modelRuntimeInitializer;

    public TemporaryModelBuilderFactory(
        IConventionSetBuilder conventionSetBuilder,
        IDesignTimeModel designTimeModel,
        IModelRuntimeInitializer modelRuntimeInitializer)
    {
        _conventionSetBuilder = conventionSetBuilder;
        _designTimeModel = designTimeModel;
        _modelRuntimeInitializer = modelRuntimeInitializer;
    }
    
    public IRelationalModel CreateRelationalModelForTemporaryEntity<TEntity>() where TEntity : class
    {
        var entityType = _designTimeModel.Model.FindEntityType(typeof(TEntity));
        if (ReferenceEquals(entityType, default))
        {
            throw new InvalidOperationException();
        }

        var temporaryTableAnnotation = entityType.FindAnnotation("TemporaryTable");
        if (ReferenceEquals(temporaryTableAnnotation, default))
        {
            throw new InvalidOperationException(); 
        }

        var configureTemporaryEntity = temporaryTableAnnotation.Value as Action<EntityTypeBuilder<TEntity>>;
        if (ReferenceEquals(configureTemporaryEntity, default))
        {
            throw new InvalidOperationException();
        }

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