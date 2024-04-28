﻿using Domain.Users.Errors;
using Infra.Common.Result;

namespace Domain.Users.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
