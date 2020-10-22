namespace Botwos.Infrastructure.Integrations.Configurations
{
    public interface IFootballDataApiConfiguration
    {
        string Token { get; }
        string BaseUri { get; }
        string Timeout { get; }
    }
}