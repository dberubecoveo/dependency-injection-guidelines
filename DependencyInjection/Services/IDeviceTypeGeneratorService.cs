using DependencyInjection.Devices;

namespace DependencyInjection.Services;

public interface IDeviceTypeGeneratorService
{
    DeviceType GetDeviceType();
}
