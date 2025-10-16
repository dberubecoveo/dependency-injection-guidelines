using DependencyInjection.Devices;

namespace DependencyInjection.Factories;

public class DeviceFactory : IDeviceFactory
{
    private IIdGenerationServiceFactory IdGenerationServiceFactory { get; }

    public DeviceFactory(IIdGenerationServiceFactory idGenerationServiceFactory)
    {
        IdGenerationServiceFactory = idGenerationServiceFactory;
    }

    public Device CreateDevice(DeviceType deviceType, double price)
    {
        var id = IdGenerationServiceFactory.GetIdGenerationService().Generate();

        return deviceType switch
        {
            DeviceType.Laptop => new Laptop(id, price),
            DeviceType.Desktop => new Desktop(id, price),
            DeviceType.Phone => new Phone(id, price),
            _ => throw new NotImplementedException($"DeviceType '{deviceType}' not implemented.")
        };
    }
}
