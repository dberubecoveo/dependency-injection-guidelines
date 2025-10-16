using DependencyInjection.Devices;

namespace DependencyInjection.Factories;

public interface IDeviceFactory
{
    Device CreateDevice(DeviceType deviceType, double price);
}
