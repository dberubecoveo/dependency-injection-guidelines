namespace DependencyInjection.Services;

public class SecondDependency : ISecondDependency
{
    public void DoOtherStuff()
    {
        Console.WriteLine($"{nameof(FirstDependency)} is doing stuff");
    }
}
