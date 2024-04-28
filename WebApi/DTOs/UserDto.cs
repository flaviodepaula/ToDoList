﻿using System.Text.Json.Serialization;

namespace WebApi.DTOs
{
    public record UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
