namespace DependencyInjection.Configuration;

public class Config : IIdGenerationConfiguration
{
    public string Prefix { get; set; }
    public string Suffix { get; set; }
}
