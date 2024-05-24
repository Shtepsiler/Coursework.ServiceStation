using PARTS.DAL.Entities.Item;

namespace PARTS.DAL.Entities.Vehicle
{
    public class Vehicle : Base
    {
        public string? FullModelName { get; set; }
        public string? VIN { get; set; }
        public DateTime? Year { get; set; }
        public Make? Make { get; set; }
        public Model? Model { get; set; }
        public SubModel? SubModel { get; set; }
        public Engine? Engine { get; set; }
        public string? URL { get; set; }
        public List<Part>? Parts { get; set; }
    }
}
