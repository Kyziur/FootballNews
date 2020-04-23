using System;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class TeamsController : BaseController
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILeagueRepository _leagueRepository;

        public TeamsController(ITeamRepository teamRepository, ILeagueRepository leagueRepository)
        {
            _teamRepository = teamRepository;
            _leagueRepository = leagueRepository;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            var teams = await _teamRepository.GetAllFiltered(search);
            var onePageTeams = await teams.ToPagedListAsync(page, 10);
            var model = onePageTeams.Select(x => new TeamModel
            {
                Id = x.Id,
                Name = x.Name,
               
            });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var leagues = await _leagueRepository.GetAll();
            var model = new TeamModel
            {
                Leagues = leagues.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamModel model)
        {
            if (!ModelState.IsValid)
            {
                var leagues = await _leagueRepository.GetAll();
                model.Leagues = leagues.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                return View(model);
            }

            var league = await _leagueRepository.GetById(Guid.Parse(model.SelectedLeagueId));
            var team = new Team(Guid.NewGuid(), model.Name, league);
            await _teamRepository.Create(team);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var team = await _teamRepository.GetById(id);
            var leagues = await _leagueRepository.GetAll();
            var model = new TeamModel
            {
                Id = team.Id,
                Name = team.Name,
                SelectedLeagueId = team.League?.Id.ToString(),
                Leagues = leagues.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TeamModel model)
        {
            if (!ModelState.IsValid)
            {
                var leagues = await _leagueRepository.GetAll();
                model.Leagues = leagues.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                return View(model);
            }

            var team = await _teamRepository.GetById(model.Id);
            team.SetName(model.Name);
            var league = await _leagueRepository.GetById(Guid.Parse(model.SelectedLeagueId));
            team.SetLeague(league);
            
            await _teamRepository.Update(team);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var league = await _teamRepository.GetById(id);
            await _teamRepository.Delete(league);
            return Ok();
        }
    }
}