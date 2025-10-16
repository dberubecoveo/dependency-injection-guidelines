namespace DependencyInjection.Devices;

public abstract class Device
{
    public string Id { get; }
    public double Price { get; }

    protected Device(string id, double price)
    {
        Id = id;
        Price = price;
    }
}
