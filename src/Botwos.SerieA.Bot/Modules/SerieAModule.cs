using System.Threading.Tasks;
using Discord.Commands;
using Botwos.Infrastructure.Integrations;
using Botwos.Infrastructure.Integrations.Exceptions.Weather;
using System.Linq;
using System.Text;

namespace Botwos.SerieA.Bot.Modules
{
    [Group("seriea")]
    public class SerieAModule : ModuleBase<SocketCommandContext>
    {
        private readonly IFootballDataApi api;
        public SerieAModule(IFootballDataApi api)
        {
            this.api = api;
        }

        [Command("")]
        [Summary("Get standings from Brasileirão")]
        async public Task GetStandingAsync()
        {
            try 
            {
                var standing = await this.api.GetStandingAsync();
                var teams = standing.Standings.First().Table.OrderBy(s => s.Position);
                var text = teams.Select(t => $"{t.Position}º - {t.Team.Name} {t.Points} pts").Aggregate((beforeString, afterString) => $"{beforeString}\n{afterString}");

                await Context.Channel.SendMessageAsync(text);
            }
            catch(StateNotFoundException ex)
            {
                await Context.Channel.SendMessageAsync(ex.Message);
            }
        }
    }
}
