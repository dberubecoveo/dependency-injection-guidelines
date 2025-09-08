using DependencyInjection.Services;

namespace DependencyInjection;

public class DependencyContainer : IDependencyContainer
{
    public IFirstDependency FirstDependency { get; set; }
    public ISecondDependency SecondDependency { get; set; }
    public IMyClass MyClass { get; set; }

    public DependencyContainer()
    {
        FirstDependency = new FirstDependency();
        SecondDependency = new SecondDependency();
        MyClass = new MyClass(this);
    }
}
