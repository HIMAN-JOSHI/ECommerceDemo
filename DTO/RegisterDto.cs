﻿using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace ECommerceDemo.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Paswords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
