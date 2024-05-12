using System.ComponentModel.DataAnnotations.Schema;

namespace JOBS.DAL.Entities
{
    public class MechanicsTasks
    {
        public Guid Id { get; set; }
        public Guid MechanicId { get; set; }
        public Guid? JobId { get; set; }
        [NotMapped]
        public Job? Job { get; set; }
        public string Task { get; set; }
        public string? Status { get; set; }
    }

}
