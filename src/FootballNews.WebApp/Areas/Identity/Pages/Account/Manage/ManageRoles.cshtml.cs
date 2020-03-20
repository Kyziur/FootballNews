using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FootballNews.WebApp.Areas.Identity.Pages.Account.Manage
{
    public class ManageRoles : PageModel
    {
        private readonly UserManager<User> _userManager;

        public ManageRoles(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public IEnumerable<UserRoleModel> UserRoleModels { get; set; }
        
        public class UserRoleModel
        {
            public Guid Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public IList<string> Roles { get; set; }
        }
        
        public async Task OnGetAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var tasks = users.Select(async user => new UserRoleModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Roles = await _userManager.GetRolesAsync(user)
                 
            });

            UserRoleModels = await Task.WhenAll(tasks);

        }
    }
}