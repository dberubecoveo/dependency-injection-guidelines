using DependencyInjection.Factories;
using DependencyInjection.Services;

namespace DependencyInjection;

public interface IDependencyContainer
{
    IIdGenerationServiceFactory IdGenerationServiceFactory { get; }
    IDeviceFactory DeviceFactory { get; }
    IDeviceTypeGeneratorService FirstDependency { get; }
    IPriceGeneratorService SecondDependency { get; }
    IMyClass MyClass { get; }
}
