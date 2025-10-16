using DependencyInjection.Factories;
using DependencyInjection.Services;

namespace DependencyInjection;

/// <summary>
/// This is the new implementation, following dependency injection guidelines.
/// </summary>
public class MyClass : IMyClass
{
    // Dependencies are defined as private properties.
    private IDeviceTypeGeneratorService DeviceTypeGenerator { get; }
    private IPriceGeneratorService PriceGenerator { get; }
    private IDeviceFactory DeviceFactory { get; }

    // This first constructor accepts the dependency container, which holds the singleton service instances.
    // When dependency injection is completely introduce, this constructor will be removed.
    public MyClass(IDependencyContainer dependencyContainer)
        : this(
            dependencyContainer.FirstDependency,
            dependencyContainer.SecondDependency,
            dependencyContainer.DeviceFactory
        ) { }

    // This second constructor accepts the different dependencies. Used by tests and will ultimately be the only one remaining
    // since DI will use it to inject the dependencies.
    public MyClass(
        IDeviceTypeGeneratorService deviceTypeGenerator,
        IPriceGeneratorService priceGenerator,
        IDeviceFactory deviceFactory
    )
    {
        DeviceTypeGenerator = deviceTypeGenerator;
        PriceGenerator = priceGenerator;
        DeviceFactory = deviceFactory;
    }

    public void Process()
    {
        Console.WriteLine($"{nameof(MyClass)} is processing");

        var deviceType = DeviceTypeGenerator.GetDeviceType();
        var price = PriceGenerator.GetPrice();

        // This code uses factories to create instances of class.
        var device = DeviceFactory.CreateDevice(deviceType, price);

        Console.WriteLine(
            $"Device created: Type={device.GetType().Name}, Id={device.Id}, Price={device.Price}$"
        );
    }
}
