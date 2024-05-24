using IDENTITY.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDENTITY.DAL.Seeding
{
    public static class Seed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            // Seed roles if needed
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new Role("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new Role("User"));
            }
            if (!await roleManager.RoleExistsAsync("Mechanic"))
            {
                await roleManager.CreateAsync(new Role("Mechanic"));
            }

            // Seed default user
            var defaultUser1 = await userManager.FindByEmailAsync("broccolicodeman.shopoyisty@gmail.com");
            if (defaultUser1 == null)
            {
                defaultUser1 = new User
                {
                    UserName = "broccolicodeman",
                    Email = "broccolicodeman.shopoyisty@gmail.com",
                    EmailConfirmed = true
                    // Add any additional properties here
                };      
                var result = await userManager.CreateAsync(defaultUser1, ",Hjrjksrjlvfy8"); // Change the password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser1, "Admin");
                }
            }

            var defaultUser2 = await userManager.FindByEmailAsync("buckwheatcodeman.shopoyisty@gmail.com");
            if (defaultUser2 == null)
            {
                 defaultUser2 = new User
                {
                     Id = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                     UserName = "buckwheatcodeman",
                    Email = "buckwheatcodeman.shopoyisty@gmail.com",
                    EmailConfirmed = true
                    // Add any additional properties here
                };
                var result = await userManager.CreateAsync(defaultUser2, ",Hjrjksrjlvfy8"); // Change the password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser2, "Mechanic");
                }
            }
            var defaultUser3 = await userManager.FindByEmailAsync("ricecodeman.shopoyisty@gmail.com");
            if (defaultUser3 == null)
            {
                defaultUser3 = new User
                {
                    UserName = "ricecodeman",
                    Email = "ricecodeman.shopoyisty@gmail.com",
                    EmailConfirmed = true
                    // Add any additional properties here
                };
                var result = await userManager.CreateAsync(defaultUser3, ",Hjrjksrjlvfy8"); // Change the password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser3, "User");
                }
            }
        }
    }
}
