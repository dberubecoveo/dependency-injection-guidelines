namespace DependencyInjection.Services;

public class FirstDependency : IFirstDependency
{
    public void DoStuff()
    {
        Console.WriteLine($"{nameof(FirstDependency)} is doing stuff");
    }
}
