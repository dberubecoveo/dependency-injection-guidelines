namespace DependencyInjection.Services;

public class IdGenerationService : IIdGenerationService
{
    public string Prefix { get; }
    public string Suffix { get; }

    public IdGenerationService(string prefix, string suffix)
    {
        Prefix = prefix;
        Suffix = suffix;
    }

    public string Generate()
    {
        return $"{Prefix}-{Guid.NewGuid()}-{Suffix}";
    }
}
