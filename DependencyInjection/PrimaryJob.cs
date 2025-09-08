namespace DependencyInjection;

public class PrimaryJob : IPrimaryJob
{
    private IMyClass MyClass { get; }

    public PrimaryJob(DependencyContainer dependencyContainer)
        : this(dependencyContainer.MyClass) { }

    public PrimaryJob(IMyClass myClass)
    {
        MyClass = myClass;
    }

    public void StartJob()
    {
        Console.WriteLine($"Starting {nameof(PrimaryJob)}");

        MyClass.Process();
    }
}
