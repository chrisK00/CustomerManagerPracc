using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerManager.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.API.Data
{
    public static class Seed
    {
        public static async Task SeedCustomersAsync(UserManager<AppUser> userManager, CustomerContext context)
        {
            if (await userManager.Users.AnyAsync())
            {
                return;
            }
            var customerData = await File.ReadAllTextAsync("Data/CustomerSeedData.json");
            var customers = JsonSerializer.Deserialize<List<AppUser>>(customerData);

            customers.ForEach(c => userManager.CreateAsync(c, "Password123."));
            await context.SaveChangesAsync();
        }
    }
}
