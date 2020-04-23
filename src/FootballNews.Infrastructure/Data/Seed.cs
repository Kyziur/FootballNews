using System;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.WebApp.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FootballNews.Infrastructure.Data
{
    public class Seed
    {
        private readonly IServiceProvider _serviceProvider;


        public Seed(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Run()
        {
            await AddRoles();
            await AddUsers();
        }

        private async Task AddRoles()
        {
            using var scope = _serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole<Guid>>>();

            if(await roleManager.Roles.AnyAsync())
                return;

            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.Admin));
            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.Redaktor));
            await roleManager.CreateAsync(new IdentityRole<Guid>(Role.User));
        }

        private async Task AddUsers()
        {
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
            
            const string username = "Admin";
            var admin = new User(username);
            var exists = await userManager.Users.AnyAsync(x => x.UserName == username);
            if(exists)
                return;
            
            const string password = "admin";
            await userManager.CreateAsync(admin,password);
            await userManager.AddToRoleAsync(admin, Role.Admin);

        }
    }
}