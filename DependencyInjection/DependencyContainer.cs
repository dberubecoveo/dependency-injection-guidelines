using DependencyInjection.Configuration;
using DependencyInjection.Factories;
using DependencyInjection.Services;

namespace DependencyInjection;

/// <summary>
/// This is a temporary container for the singleton instances and factories to be used throughout the application.
/// Its purpose is to avoid having to pass the different dependencies across the entire hierarchy while being able
/// to use singleton instances that are not static.
/// </summary>
public class DependencyContainer : IDependencyContainer
{
    public Config Configuration { get; }
    public IIdGenerationServiceFactory IdGenerationServiceFactory { get; }
    public IDeviceFactory DeviceFactory { get; }
    public IDeviceTypeGeneratorService FirstDependency { get; }
    public IPriceGeneratorService SecondDependency { get; }
    public IMyClass MyClass { get; }

    public DependencyContainer(Config configuration)
    {
        Configuration = configuration;

        IdGenerationServiceFactory = new IdGenerationServiceFactory(configuration);
        DeviceFactory = new DeviceFactory(IdGenerationServiceFactory);
        FirstDependency = new DeviceTypeGeneratorService();
        SecondDependency = new PriceGeneratorService();
        MyClass = new MyClass(this);
    }
}
