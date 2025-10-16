namespace DependencyInjection.Services;

public class PrimaryJobService : IPrimaryJobService
{
    // Dependencies are defined as private properties.
    private IMyClass MyClass { get; }

    // This first constructor accepts the dependency container, which holds the singleton service instances.
    // When dependency injection is completely introduce, this constructor will be removed.
    public PrimaryJobService(DependencyContainer dependencyContainer)
        : this(dependencyContainer.MyClass) { }

    // This second constructor accepts the different dependencies. Used by tests and will ultimately be the only one remaining
    // since DI will use it to inject the dependencies.
    public PrimaryJobService(IMyClass myClass)
    {
        MyClass = myClass;
    }

    public void StartJob()
    {
        Console.WriteLine($"Starting {nameof(PrimaryJobService)}");

        MyClass.Process();
    }
}
