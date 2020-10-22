using System.Threading.Tasks;
using Botwos.Infrastructure.Integrations.Models.FootballData;

namespace Botwos.Infrastructure.Integrations
{
    public interface IFootballDataApi
    {
        Task<StandingAggregateResponse> GetStandingAsync();
    }
}