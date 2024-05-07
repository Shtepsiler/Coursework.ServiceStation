using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using IDENTITY.DAL.Entities;
using IDENTITY.DAL.Data.Configurations;

namespace IDENTITY.DAL.Data
{
    public class AppDBContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDBContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

        }

    }
}
