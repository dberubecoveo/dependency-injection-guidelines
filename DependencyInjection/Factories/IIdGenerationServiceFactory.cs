using DependencyInjection.Services;

namespace DependencyInjection.Factories;

public interface IIdGenerationServiceFactory
{
    IIdGenerationService GetIdGenerationService();
}
