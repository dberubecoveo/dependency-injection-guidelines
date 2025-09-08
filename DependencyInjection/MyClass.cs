using DependencyInjection.Services;

namespace DependencyInjection;

public class MyClass : IMyClass
{
    private IFirstDependency FirstDependency { get; }
    private ISecondDependency SecondDependency { get; }

    public MyClass(IDependencyContainer dependencyContainer)
        : this(dependencyContainer.FirstDependency, dependencyContainer.SecondDependency) { }

    public MyClass(IFirstDependency firstDependency, ISecondDependency secondDependency)
    {
        FirstDependency = firstDependency;
        SecondDependency = secondDependency;
    }

    public void Process()
    {
        Console.WriteLine($"{nameof(MyLegacyClass)} is processing");

        FirstDependency.DoStuff();
        SecondDependency.DoOtherStuff();
    }
}
