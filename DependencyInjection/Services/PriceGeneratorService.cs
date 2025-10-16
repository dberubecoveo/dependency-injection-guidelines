namespace DependencyInjection.Services;

public class PriceGeneratorService : IPriceGeneratorService
{
    public double GetPrice()
    {
        // Dummy implementation that returns a random price.
        var price = new Random().Next(0, 1000) / (double)10;

        Console.WriteLine($"{nameof(PriceGeneratorService)} determining a price => {price}$.");

        return price;
    }
}
