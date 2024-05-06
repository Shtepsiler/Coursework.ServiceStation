namespace PARTS.DAL.Entities.Vehicle
{
    public class Engine : Base
    {
        public int Cylinders { get; set; }
        public int Liter { get; set; }
        public DateTime? Year { get; set; }
        public string? Model { get; set; }
        public SubModel? SubModel { get; set; }
        public Make? Make { get; set; }
        public List<Vehicle>? Vehicles { get; set; }

    }
}
