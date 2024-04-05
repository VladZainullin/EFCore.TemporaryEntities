using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EFCore.TemporaryEntities.Design;

public sealed class ExcludeTemporaryEntityAnnotationCodeGenerator(AnnotationCodeGeneratorDependencies dependencies)
    : AnnotationCodeGenerator(dependencies)
{
    public override IEnumerable<IAnnotation> FilterIgnoredAnnotations(IEnumerable<IAnnotation> annotations) =>
        base.FilterIgnoredAnnotations(annotations)
            .Where(a => a.Name != "TemporaryEntity");
}