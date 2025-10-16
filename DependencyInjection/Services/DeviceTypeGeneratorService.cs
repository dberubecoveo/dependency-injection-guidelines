using DependencyInjection.Devices;

namespace DependencyInjection.Services;

public class DeviceTypeGeneratorService : IDeviceTypeGeneratorService
{
    public DeviceType GetDeviceType()
    {
        // Dummy implementation that returns a device type.
        var deviceType = DeviceType.Laptop;

        Console.WriteLine(
            $"{nameof(DeviceTypeGeneratorService)} is deciding which {nameof(DeviceType)} to use => {deviceType}."
        );

        return deviceType;
    }
}
