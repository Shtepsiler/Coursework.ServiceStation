﻿namespace PARTS.BLL.DTOs.Responses
{
    public class MakeResponse : BaseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Year { get; set; }

        public List<VehicleResponse>? Vehicles { get; set; }
        public List<ModelResponse>? Models { get; set; }
    }
}
