﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TreasureGuide.Web.Constants;

namespace TreasureGuide.Web.Configurations
{
    public static class RoleConfig
    {
        public static async void Configure(RoleManager<IdentityRole> roleManager)
        {
            await AddIfNotExists(roleManager, RoleConstants.Administrator);
            await AddIfNotExists(roleManager, RoleConstants.Moderator);
            await AddIfNotExists(roleManager, RoleConstants.Contributor);
        }

        private static async Task AddIfNotExists(RoleManager<IdentityRole> roleManager, string role)
        {
            if (await roleManager.FindByNameAsync(role) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}