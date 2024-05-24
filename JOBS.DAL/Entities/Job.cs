using System.ComponentModel.DataAnnotations.Schema;

namespace JOBS.DAL.Entities
{
    public class Job
    {
        public Guid Id { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? ModelId { get; set; }
        public string? ModelName { get; set; }
        public string? Status { get; set; }
        public Guid? ClientId { get; set; }
        public Guid? MechanicId { get; set; }
        public Guid? OrderId { get; set; } 
        public DateTime IssueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public List<MechanicsTasks>? Tasks { get; set; }
    }
}
