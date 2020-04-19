using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
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
                AwayTeamScore = x.AwayTeamScore(),
                HomeTeamScore = x.HomeTeamScore()
            });

            return View(model);
        }

        private async Task<IList<SelectListItem>> GetAllTeamsAsSelectList()
        {
            var teams = await _teamRepository.GetAll();
            return await teams.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToListAsync();
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

        private async Task<IList<SelectListItem>> GetPlayersByTeamAsSelectList(string team)
        {
            var players = await _playerRepository.GetByTeam(team);
            return players.Select(x => new SelectListItem($"{x.FirstName} {x.LastName}", x.Id.ToString())).ToList();
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
            List<Goal> homeTeamGoals = new List<Goal>();
            List<Goal> awayTeamGoals = new List<Goal>();
            if (model.HomeTeamGoals != null && model.HomeTeamGoals.Any())
            {
               var homeGoals = await GetGoalsFromGameModel(model.HomeTeamGoals, game);
               homeTeamGoals = homeGoals.ToList();
            }

            if (model.AwayTeamGoals != null && model.AwayTeamGoals.Any())
            {
                var awayGoals = await GetGoalsFromGameModel(model.AwayTeamGoals, game);
                awayTeamGoals = awayGoals.ToList();
            }
            var goalsOverall = homeTeamGoals.Concat(awayTeamGoals).ToList();
            game.Goals = goalsOverall;
            game.UpdateTeamsPoints();

            await _gameRepository.Create(game);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var game = await _gameRepository.GetById(id);
            
            var gameModel = new GameModel
            {
                Id = game.Id,
                Date = game.Date,
                Teams = await GetAllTeamsAsSelectList(),
                AwayTeamGoals = game.Goals.Where(x => x.Team.Id == game.AwayTeamId).Select(y => new GoalModel
                {
                    ShooterName = $"{y.Shooter.FirstName} {y.Shooter.LastName}",
                    Shooter = y.Shooter.Id.ToString(),
                    Time = (int)y.Time
                }).ToList(),
                AwayTeamPlayers = await GetPlayersByTeamAsSelectList(game.AwayTeam.Id.ToString()),
                HomeTeamGoals = game.Goals.Where(x => x.Team.Id == game.HomeTeamId).Select(y => new GoalModel
                {
                    ShooterName = $"{y.Shooter.FirstName} {y.Shooter.LastName}",
                    Shooter = y.Shooter.Id.ToString(),
                    Time = (int)y.Time
                }).ToList(),
                HomeTeamPlayers = await GetPlayersByTeamAsSelectList(game.HomeTeam.Id.ToString()),
                Report = game.Report,
                SelectedAwayTeam = game.AwayTeamId.ToString(),
                SelectedHomeTeam = game.HomeTeamId.ToString()    
            };

            return View(gameModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GameModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var game = await _gameRepository.GetById(model.Id);
            game.SetReport(model.Report);
            
            
            //TODO: If game that was in past and its score has been already calculated then change logic of
            //UpdateTeamsPoints because right now it will calculate points again on update if score changed.
            //It should involve removing points that has been already added because it will result with multiple points added for one game.
            if (model.HomeTeamGoals.Count != game.HomeTeamScore() || model.AwayTeamGoals.Count != game.AwayTeamScore())
            {
                var homeTeamGoals = await GetGoalsFromGameModel(model.HomeTeamGoals, game);
                var awayTeamGoals = await GetGoalsFromGameModel(model.AwayTeamGoals, game);
                var goalsOverall = homeTeamGoals.Concat(awayTeamGoals).ToList();
                game.Goals = goalsOverall;
                game.UpdateTeamsPoints();
            }
            
            await _gameRepository.Update(game);
            return RedirectToAction(nameof(Index));
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var league = await _gameRepository.GetById(id);
                await _gameRepository.Delete(league);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not delete game with id: {id}");
                return BadRequest();
            }

            return Ok();
        }
    }
}