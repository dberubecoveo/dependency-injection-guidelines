using DependencyInjection.Configuration;
using DependencyInjection.Services;

namespace DependencyInjection.Factories;

public class IdGenerationServiceFactory : IIdGenerationServiceFactory
{
    private IIdGenerationConfiguration Configuration { get; }

    public IdGenerationServiceFactory(IIdGenerationConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IIdGenerationService GetIdGenerationService()
    {
        return new IdGenerationService(Configuration.Prefix, Configuration.Suffix);
    }
}
