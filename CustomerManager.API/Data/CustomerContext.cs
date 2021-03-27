using CustomerManager.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.API.Data
{
    public class CustomerContext : IdentityDbContext<AppUser>
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }
    }
}
