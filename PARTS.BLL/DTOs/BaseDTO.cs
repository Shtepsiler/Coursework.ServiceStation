namespace PARTS.BLL.DTOs
{
    public abstract class BaseDTO
    {
        Guid Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

    }
}
