using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore.TemporaryEntities.Design;

public sealed class TemporaryEntityDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IAnnotationCodeGenerator, ExcludeTemporaryEntityAnnotationCodeGenerator>();
    }
}