using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FootballNews.WebApp.Components
{
    public class LeagueScoreTableViewComponent : ViewComponent
    {
        private readonly ITeamRepository _teamRepository;

        public LeagueScoreTableViewComponent(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(string leagueName)
        {
            var teams = await _teamRepository.GetByLeagueName(leagueName);

            var model = new LeagueScoreTableModel
            {
                League = leagueName,
                TeamScores = teams.Select(x => new TeamScoreModel(x.Name, x.Points)).ToList()
            };
            
            return View("SideLeagueScoreTable",model);
        }
        
    }
}