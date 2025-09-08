namespace DependencyInjection;

public class MainProcess
{
    public static void Main()
    {
        Console.WriteLine("Starting application");

        var container = new DependencyContainer();

        var primaryJob = new PrimaryJob(container);

        primaryJob.StartJob();
    }
}
