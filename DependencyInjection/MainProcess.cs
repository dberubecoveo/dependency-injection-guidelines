using DependencyInjection.Configuration;
using DependencyInjection.Services;

namespace DependencyInjection;

public class MainProcess
{
    public static void Main()
    {
        Console.WriteLine("Starting application");

        var configuration = new Config { Prefix = "SomePrefix", Suffix = "SomeSuffix" };

        // The temporary dependency container is created at the root and passed through the hierarchy.
        // Ultimately, it would be removed since those dependencies are held in the real DI container.
        var container = new DependencyContainer(configuration);

        var primaryJob = new PrimaryJobService(container);

        primaryJob.StartJob();
    }
}
