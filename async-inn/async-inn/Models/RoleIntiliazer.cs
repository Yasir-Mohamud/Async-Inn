using async_inn.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models
{
    public class RoleIntiliazer
    {
        // creates list of identity roles
        private static List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole { Name = ApplicationRoles.DistrictManager, NormalizedName = ApplicationRoles.DistrictManager.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Name = ApplicationRoles.PropertyManager, NormalizedName = ApplicationRoles.PropertyManager.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole { Name = ApplicationRoles.Agent, NormalizedName = ApplicationRoles.Agent.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() },
        };
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AsyncInnDbContext(serviceProvider.GetRequiredService<DbContextOptions<AsyncInnDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager, IConfiguration _config)
        {
            if (userManager.FindByEmailAsync(_config["AdminEmail"]).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = _config["AdminEmail"];
                user.Email = _config["AdminEmail"];
                user.FirstName = "Yasir";
                user.LastName = "Mohamud";

                IdentityResult result = userManager.CreateAsync(user, _config["AdminPassword"]).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, ApplicationRoles.DistrictManager).Wait();
                }
            }
        }

        private static void AddRoles(AsyncInnDbContext context)
        {
            if (context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
}
