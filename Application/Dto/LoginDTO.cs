﻿using System.ComponentModel.DataAnnotations;

namespace NexusGym.Application.Dto
{
    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
