#pragma warning disable CA1859

using DependencyInjection.Services;

namespace DependencyInjection;

public class MyLegacyClass : IMyClass
{
    private readonly IFirstDependency _firstDependency;

    public MyLegacyClass()
    {
        _firstDependency = new FirstDependency();
    }

    public void Process()
    {
        Console.WriteLine($"{nameof(MyLegacyClass)} is processing");

        _firstDependency.DoStuff();
        new SecondDependency().DoOtherStuff();
    }
}
