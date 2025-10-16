namespace DependencyInjection.Configuration;

public interface IIdGenerationConfiguration
{
    string Prefix { get; }
    string Suffix { get; }
}
