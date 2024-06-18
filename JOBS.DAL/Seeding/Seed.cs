using Microsoft.Extensions.DependencyInjection;
using JOBS.DAL.Entities;
using JOBS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOBS.DAL.Seeding
{
    public static class Seed
    {
        // Define static IDs for Jobs and Tasks
        private static readonly Guid MechanicId = Guid.Parse("dc238098-d410-44f3-778e-08dc7b9965ae");
        private static readonly Guid ModelId1 = Guid.Parse("b5a0c2e2-3d4f-4dd3-b499-98d7e16a5360");
        private static readonly Guid ModelId2 = Guid.Parse("88c2a122-9e71-4a7a-a52d-9f82a6610d87");

        private static readonly Guid JobId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid JobId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
        private static readonly Guid JobId3 = Guid.Parse("33333333-3333-3333-3333-333333333333");

        private static readonly Guid TaskId1Job1 = Guid.Parse("44444444-4444-4444-4444-444444444444");
        private static readonly Guid TaskId2Job1 = Guid.Parse("55555555-5555-5555-5555-555555555555");
        private static readonly Guid TaskId3Job1 = Guid.Parse("66666666-6666-6666-6666-666666666666");

        private static readonly Guid TaskId1Job2 = Guid.Parse("77777777-7777-7777-7777-777777777777");
        private static readonly Guid TaskId2Job2 = Guid.Parse("88888888-8888-8888-8888-888888888888");
        private static readonly Guid TaskId3Job2 = Guid.Parse("99999999-9999-9999-9999-999999999999");

        private static readonly Guid TaskId1Job3 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        private static readonly Guid TaskId2Job3 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        private static readonly Guid TaskId3Job3 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ServiceStationDBContext>();

            if (context.Jobs.Any())
            {
                return;   // DB has been seeded
            }

            // Create job list
            var jobs = new List<Job>
            {
                new Job
                {
                    Id = JobId1,
                    ModelId = ModelId1,
                    IssueDate = DateTime.Now,
                    Status = "NewJob",
                    MechanicId = MechanicId
                },
                new Job
                {
                    Id = JobId2,
                    ModelId = ModelId2,
                    IssueDate = DateTime.Now,
                    Status = "InProgress",
                    MechanicId = MechanicId
                },
                new Job
                {
                    Id = JobId3,
                    ModelId = ModelId1,
                    IssueDate = DateTime.Now,
                    Status = "Completed",
                    MechanicId = MechanicId
                }
            };

            // Add jobs to the context
            context.Jobs.AddRange(jobs);
            await context.SaveChangesAsync();

            // Create task list for each job
            var tasks = new List<MechanicsTasks>
            {
                new MechanicsTasks
                {
                    Id = TaskId1Job1,
                    JobId = JobId1,
                    Name = "Task 1 for Job 1",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 1",
                    Status = "Pending",
                    MechanicId = MechanicId
                },
                new MechanicsTasks
                {
                    Id = TaskId2Job1,
                    JobId = JobId1,
                    Name = "Task 2 for Job 1",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 2",
                    Status = "Pending",
                    MechanicId = MechanicId
                },
                new MechanicsTasks
                {
                    Id = TaskId3Job1,
                    JobId = JobId1,
                    Name = "Task 3 for Job 1",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 3",
                    Status = "Pending",
                    MechanicId = MechanicId
                },
                new MechanicsTasks
                {
                    Id = TaskId1Job2,
                    JobId = JobId2,
                    Name = "Task 1 for Job 2",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 1",
                    Status = "Pending",
                    MechanicId = MechanicId
                },
                new MechanicsTasks
                {
                    Id = TaskId2Job2,
                    JobId = JobId2,
                    Name = "Task 2 for Job 2",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 2",
                    Status = "Pending",
                    MechanicId = MechanicId
                },
                new MechanicsTasks
                {
                    Id = TaskId3Job2,
                    JobId = JobId2,
                    Name = "Task 3 for Job 2",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 3",
                    Status = "Pending",
                    MechanicId = MechanicId
                },
                new MechanicsTasks
                {
                    Id = TaskId1Job3,
                    JobId = JobId3,
                    Name = "Task 1 for Job 3",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 1",
                    Status = "Pending",
                    MechanicId = MechanicId
                },
                new MechanicsTasks
                {
                    Id = TaskId2Job3,
                    JobId = JobId3,
                    Name = "Task 2 for Job 3",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 2",
                    Status = "Pending",
                    MechanicId = MechanicId
                },
                new MechanicsTasks
                {
                    Id = TaskId3Job3,
                    JobId = JobId3,
                    Name = "Task 3 for Job 3",
                    IssueDate = DateTime.Now,
                    Task = "Description for Task 3",
                    Status = "Pending",
                    MechanicId = MechanicId
                }
            }; 
            context.MechanicsTasks.AddRange(tasks);
                  await context.SaveChangesAsync();
            context.Jobs.Where(x => x.Id == JobId1).FirstOrDefault().Tasks = context.MechanicsTasks.Take(3).ToList();
            context.Jobs.Where(x => x.Id == JobId2).FirstOrDefault().Tasks = context.MechanicsTasks.Skip(3).Take(3).ToList();
            context.Jobs.Where(x => x.Id == JobId3).FirstOrDefault().Tasks = context.MechanicsTasks.Skip(6).Take(3).ToList();

            // Add tasks to the context

            await context.SaveChangesAsync();

        }
    }
}
