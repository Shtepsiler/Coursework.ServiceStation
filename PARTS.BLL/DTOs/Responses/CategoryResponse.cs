﻿namespace PARTS.BLL.DTOs.Responses
{
    public class CategoryResponse : BaseDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public CategoryImageResponse? CategoryImage { get; set; }
        public List<PartResponse> Parts { get; set; }

    }
}
