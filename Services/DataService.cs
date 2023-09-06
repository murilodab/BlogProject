using BlogProject.Data;
using BlogProject.Enums;
using BlogProject.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Services
{
    public class DataService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext,
                           RoleManager<IdentityRole> roleManager,
                           UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task ManageDataAsync() //Wrapper
        {
            //Seed a few Roles into the system
            await SeedRolesAsync();

            //Seed a few users into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            //If there are already Roles in the system, do nothing.
            if (_dbContext.Roles.Any())
            {
                return;
            }

            //Otherwise we want to create a few Roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                //Use the Role Manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));

            }
        }

        private async Task SeedUsersAsync()
        {
            //If there are already any Users in the system, do nothing.
            if (_dbContext.Users.Any())
            {
                return;
            }

            //Otherwise we want to create a few users
            //Admin
            //Step 1: Creates a new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "mbtestcoder@gmail.com",
                UserName = "mbtestcoder@gmail.com",
                FirstName = "Murilo",
                LastName = "Barbosa",
                PhoneNumber = "+55 (31) 99354-5782",
                EmailConfirmed = true
            };

            //Step 2: Use the UserManager to create a new user that is defined by the adminUser
            await _userManager.CreateAsync(adminUser, "@Admin5023");

            //Step 3: Add this new user to the Administrator Role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            //Moderator
            //Step 1:
            var modUser = new BlogUser()
            {
                Email = "muriloab96@gmail.com",
                UserName = "muriloab96@gmail.com",
                FirstName = "Murilo",
                LastName = "Barbosa",
                PhoneNumber = "+55 (31) 99354-5782",
                EmailConfirmed = true
            };

            //Step 2:
            await _userManager.CreateAsync(modUser, "@Mod5023");

            //Step 3:
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        
        
        
        }




    }
}
