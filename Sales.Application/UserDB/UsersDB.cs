using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sales.Application.UserDB
{
    public class User
    {
        public string Name;
        public string Password;
    }
    public class UsersDB : IdentityDbContext<IdentityUser>
    {
        public UsersDB(DbContextOptions<UsersDB> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "director", NormalizedName = "director" },
                new { Id = "2", Name = "manager", NormalizedName = "manager" },
                new { Id = "3", Name = "saleman", NormalizedName = "saleman" }
            );
        }
    }
}