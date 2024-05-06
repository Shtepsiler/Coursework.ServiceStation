namespace PARTS.DAL.Entities.Vehicle
{
    public class Model : Base
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Year { get; set; }
        public Make? Make { get; set; }
        public List<Vehicle>? Vehicles { get; set; }
        public List<SubModel>? SubModels { get; set; }

    }
}
