namespace PARTS.DAL.Entities.Vehicle
{
    public class Make : Base
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Year { get; set; }

        public List<Vehicle>? Vehicles { get; set; }
        public List<Model>? Models { get; set; }
        public List<Engine>? Engines { get; set; }
    }
}
