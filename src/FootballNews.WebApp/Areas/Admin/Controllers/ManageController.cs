using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.WebApp.Areas.Admin.ViewModels.Manage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class ManageController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public ManageController(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var users = _userManager.Users.ToPagedList(page, 10);
            var models = new List<IndexViewModel>();
            foreach (var user in users)
            {
                models.Add(new IndexViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = await _userManager.GetRolesAsync(user),
                    Username = user.UserName
                });
            }

            var model = await  models.ToPagedListAsync(page, 10);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();
            var model = new UpdateViewModel
            {
                Id = user.Id, Email = user.Email, Username = user.UserName,
                Roles = roles.Select(x => new SelectListItem(x.Name, x.Name)).ToList(),
                SelectedRoles = userRoles,
            };
            
            return View(model);;
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            user.Email = model.Email;
            user.UserName = model.Username;
            
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, model.SelectedRoles);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                return BadRequest($"User with id {id} does not exists.");
            }

            await _userManager.DeleteAsync(user);

            return Ok();
        }
    }
}