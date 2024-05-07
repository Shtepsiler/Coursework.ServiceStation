﻿using System.Text.Json.Serialization;

namespace IDENTITY.BLL.DTO.Requests
{
    public class UserSignInRequest
    {
        [JsonIgnore]
        public string? refererUrl { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
