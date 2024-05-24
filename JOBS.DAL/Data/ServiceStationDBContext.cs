using Microsoft.EntityFrameworkCore;
using JOBS.DAL.Entities;
using JOBS.DAL.Data.Configurations;

namespace JOBS.DAL.Data
{
    public class ServiceStationDBContext : DbContext
    {
        public ServiceStationDBContext(DbContextOptions contextOptions) : base(contextOptions)
        {


            Database.EnsureCreated();



        }


        public DbSet<Job> Jobs { get; set; }
        public DbSet<MechanicsTasks> MechanicsTasks { get; set; }

 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new JobConfiguration());
            modelBuilder.ApplyConfiguration(new MechanicsTasksConfiguration());
   

        }


    }
}
