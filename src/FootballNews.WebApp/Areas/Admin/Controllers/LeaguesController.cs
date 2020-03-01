using System;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.League;
using Microsoft.AspNetCore.Mvc;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class LeaguesController : BaseController
    {
        private readonly ILeagueRepository _leagueRepository;

        public LeaguesController(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var leagues = await _leagueRepository.GetAll();
            var model = leagues.Select(x => new LeagueModel
            {
                Id = x.Id,
                Name = x.Name
            });
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var league = await _leagueRepository.GetById(id);
            if (league is null)
            {
                return NotFound();
            }

            var model = new LeagueModel {Id = league.Id, Name = league.Name};
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeagueModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var league = new League(model.Name);
            await _leagueRepository.Create(league);
            
            //redirect to created league or to index action
            return RedirectToAction(nameof(Get), new {id = league.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var league = await _leagueRepository.GetById(id);
            var model = new LeagueModel {Id = league.Id, Name = league.Name};

            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        } 
    }
}