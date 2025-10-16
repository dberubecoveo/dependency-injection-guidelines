#pragma warning disable CA1859

using DependencyInjection.Configuration;
using DependencyInjection.Devices;
using DependencyInjection.Factories;
using DependencyInjection.Services;

namespace DependencyInjection;

/// <summary>
/// This is the legacy implementation, where dependency injection guidelines aren't used.
/// </summary>
public class MyLegacyClass : IMyClass
{
    // Dependencies are private members instead of private properties.
    private readonly IDeviceTypeGeneratorService _deviceTypeGenerator;
    private readonly IIdGenerationServiceFactory _idGenerationServiceFactory;

    // Constructor does not receive dependencies, except for the config in this case.
    public MyLegacyClass(Config configuration)
    {
        // Dependencies are initialized in the constructor, instead of being provided.
        _deviceTypeGenerator = new DeviceTypeGeneratorService();
        _idGenerationServiceFactory = new IdGenerationServiceFactory(configuration);
    }

    public void Process()
    {
        Console.WriteLine($"{nameof(MyLegacyClass)} is processing");

        var deviceType = _deviceTypeGenerator.GetDeviceType();
        var price = new PriceGeneratorService().GetPrice();

        var id = _idGenerationServiceFactory.GetIdGenerationService().Generate();

        // Objects are created directly, instead of using a factory.
        Device device = deviceType switch
        {
            DeviceType.Laptop => new Laptop(id, price),
            DeviceType.Desktop => new Desktop(id, price),
            DeviceType.Phone => new Phone(id, price),
            _ => throw new NotImplementedException($"DeviceType '{deviceType}' not implemented."),
        };

        Console.WriteLine(
            $"Device created: Type={device.GetType().Name}, Id={device.Id}, Price={device.Price}"
        );
    }
}
