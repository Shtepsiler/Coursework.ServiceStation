namespace PARTS.BLL.DTOs.Responses
{
    public class VehicleResponse : BaseDTO
    {
        public string VIN { get; set; }
        public DateTime? Year { get; set; }
        public MakeResponse? Make { get; set; }
        public ModelResponse? Model { get; set; }
        public SubModelResponse? SubModel { get; set; }
        public EngineResponse? Engine { get; set; }
        public string? URL { get; set; }
        public List<PartResponse> Parts { get; set; }
    }
}
