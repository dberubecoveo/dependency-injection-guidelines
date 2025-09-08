using DependencyInjection.Services;

namespace DependencyInjection;

public interface IDependencyContainer
{
    IFirstDependency FirstDependency { get; set; }
    ISecondDependency SecondDependency { get; set; }
    IMyClass MyClass { get; set; }
}
