using FootballNews.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballNews.WebApp.Components
{
    public class EndMatchesViewComponent: ViewComponent
    {
        private readonly IGameRepository _gameRepository;

        public EndMatchesViewComponent(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var upcomingMatches = await _gameRepository.GetAlreadyPlayed();
            var model = new UpcomingMatchesModel
            {
                Matches = upcomingMatches.Select(x => new MatchModel
                {
                    HomeTeam = x.HomeTeam.Name,
                    AwayTeam = x.AwayTeam.Name,
                    Date = x.Date,
                    HomeTeamScore = x.HomeTeamScore(),
                    AwayTeamScore = x.AwayTeamScore()
                }).ToList()
            };

            return View("EndMatches", model);
        }
    }
}
