using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FootballNews.WebApp.Components
{
    public class UpcomingMatchesViewComponent : ViewComponent
    {
        private readonly IGameRepository _gameRepository;

        public UpcomingMatchesViewComponent(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var upcomingMatches = await _gameRepository.GetUpcoming();
            var model = new UpcomingMatchesModel
            {
                Matches = upcomingMatches.Select(x => new MatchModel
                {
                    HomeTeam = x.HomeTeam.Name,
                    AwayTeam = x.AwayTeam.Name,
                    Date = x.Date
                }).ToList()
            };

            return View("UpcomingMatches",model);
        }
    }
}