using System;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.Game;
using FootballNews.WebApp.Areas.Admin.ViewModels.Player;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using X.PagedList;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class PlayersController : BaseController
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public PlayersController(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            var tags = await _playerRepository.GetAllFiltered(search);
            var onePageTags = await tags.ToPagedListAsync(page, 10);
            var model = onePageTags.Select(x => new IndexPlayerModel
            {
                Id = x.Id,
                FullName = $"{x.FirstName} {x.LastName}",
                BirthDate=x.Birthdate,
                Position = x.Position,
                Height=x.Height,
                Team = x.Team.Name
            });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var teams = await _teamRepository.GetAll();
            var model = new PlayerModel
            {
                Teams = teams.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlayerModel model)
        {
            if (!ModelState.IsValid)
            {
                var teams = await _teamRepository.GetAll();
                model.Teams = teams.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                return View(model);
            }

            var team = await _teamRepository.GetById(Guid.Parse(model.SelectedTeamId));
            var player = new Player(Guid.NewGuid(), model.FirstName, model.LastName,model.Position, model.Birthdate, team);
            player.SetHeight(model.Height);
            player.SetNumberOnTShirt(model.NumberOnShirt);

            await _playerRepository.Create(player);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var player = await _playerRepository.GetById(id);
            var teams = await _teamRepository.GetAll();
            var model = new PlayerModel
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Position = player.Position,
                Birthdate = player.Birthdate,
                NumberOnShirt = player.NumberOnTShirt,
                Height = player.Height,
                Teams = teams.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                SelectedTeamId = player.Team.Id.ToString()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PlayerModel model)
        {
            if (!ModelState.IsValid)
            {
                var teams = await _teamRepository.GetAll();
                model.Teams = teams.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                return View(model);
            }

            var player = await _playerRepository.GetById(model.Id);
            player.SetName(model.FirstName, model.LastName);
            player.SetBirthdate(model.Birthdate);
            player.SetHeight(model.Height);
            player.SetNumberOnTShirt(model.NumberOnShirt);
            var team = await _teamRepository.GetById(Guid.Parse(model.SelectedTeamId));
            if (team.Name != player.Team.Name)
            {
                player.TransferTo(team);
            }

            await _playerRepository.Update(player);

            return RedirectToAction(nameof(Index));
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var player = await _playerRepository.GetById(id);
                await _playerRepository.Delete(player);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Nie można usunąć zawodnika o podanym id: {id}");
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetByTeam(string team)
        {
            var players = await _playerRepository.GetByTeam(team);
            var playersData =
                players.Select(x => new PlayerJsonModel(x.Id.ToString(), x.FirstName, x.LastName));
            var playersAsJson = JsonConvert.SerializeObject(playersData);
            return Ok(playersAsJson);
        }
    }
}