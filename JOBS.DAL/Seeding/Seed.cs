using JOBS.DAL.Data;
using JOBS.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOBS.DAL.Seeding
{
    public static class Seed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ServiceStationDBContext>();

            if (context.Jobs.Any())
            {
                return;   // DB has been seeded
            }

            var jobs = new List<Job>
    {
        new Job
        {
            Id = Guid.NewGuid(),
            ModelId = Guid.Parse("b5a0c2e2-3d4f-4dd3-b499-98d7e16a5360"),
            IssueDate = DateTime.Now,
            Status = "NewJob",
            MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
            Tasks = new List<MechanicsTasks>
            {
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 1 for Job 1",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 1",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                },
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 2 for Job 1",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 2",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                },
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 3 for Job 1",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 3",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                }
            }
        },
        new Job
        {
            Id = Guid.NewGuid(),
            ModelId = Guid.Parse("88c2a122-9e71-4a7a-a52d-9f82a6610d87"),
            IssueDate = DateTime.Now,
            Status = "InProgress",
            MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
            Tasks = new List<MechanicsTasks>
            {
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 1 for Job 2",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 1",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                },
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 2 for Job 2",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 2",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                },
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 3 for Job 2",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 3",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                }
            }
        },
        new Job
        {
            Id = Guid.NewGuid(),
            ModelId = Guid.Parse("b5a0c2e2-3d4f-4dd3-b499-98d7e16a5360"),
            IssueDate = DateTime.Now,
            Status = "Completed",
            MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
            Tasks = new List<MechanicsTasks>
            {
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 1 for Job 3",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 1",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                },
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 2 for Job 3",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 2",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                },
                new MechanicsTasks
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 3 for Job 3",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 3",
                    Status = "Pending",
                    MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae"),
                }
            }
        }
    };

            context.Jobs.AddRange(jobs);
            await context.SaveChangesAsync();


        }
    }

}
