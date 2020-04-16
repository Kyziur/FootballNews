using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using Goal = FootballNews.Core.Domain.Goal;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGameRepository _gameRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;

        public GamesController(IGameRepository gameRepository, ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            _gameRepository = gameRepository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            var games = await _gameRepository.GetAllFiltered(search);
            var onePageGames = await games.ToPagedListAsync(page, 10);
            var model = PagedListExtensions.Select(onePageGames, x => new IndexGameModel
            {
                Id = x.Id,
                Date = x.Date.ToShortDateString(),
                AwayTeam = x.AwayTeam.Name,
                HomeTeam = x.HomeTeam.Name,
                AwayTeamScore = x.AwayTeamScore,
                HomeTeamScore = x.HomeTeamScore
            });

            return View(model);
        }

        private async Task<IList<SelectListItem>> GetAllTeamsAsSelectList()
        {
            var teams = await _teamRepository.GetAll();
            return await teams.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToListAsync();
        }

        private async Task<IList<SelectListItem>> GetPlayersAsSelectList()
        {
            var players = await _playerRepository.GetAll();
            return await players.Select(x => new SelectListItem($"{x.LastName} {x.FirstName}", x.Id.ToString()))
                .ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new GameModel
            {
                Teams = await GetAllTeamsAsSelectList(),
            };
            
            return View(model);
        }

        private async Task<IEnumerable<Goal>> GetGoalsFromGameModel(IList<GoalModel> model, Game game)
        {
            var goals = new List<Goal>();
            foreach (var goal in model)
            {
                var shooter = await _playerRepository.GetById(Guid.Parse(goal.Shooter));
                goals.Add(new Goal(Guid.NewGuid(), shooter, goal.Time, game));
            }

            return goals;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Teams = await GetAllTeamsAsSelectList();
                return View(model);
            }

            var homeTeam = await _teamRepository.GetById(Guid.Parse(model.SelectedHomeTeam));
            var awayTeam = await _teamRepository.GetById(Guid.Parse(model.SelectedAwayTeam));

            var game = new Game(Guid.NewGuid(), homeTeam, awayTeam, model.Date);
            game.SetReport(model.Report);

            var homeTeamGoals = await GetGoalsFromGameModel(model.HomeTeamGoals, game);
            var awayTeamGoals = await GetGoalsFromGameModel(model.AwayTeamGoals, game);
            var goalsOverall = homeTeamGoals.Concat(awayTeamGoals);
            //TODO: Change to set instead
            game.Goals = goalsOverall;
                
            await _gameRepository.Create(game);
            
            return RedirectToAction(nameof(Index));
        }
    }
}