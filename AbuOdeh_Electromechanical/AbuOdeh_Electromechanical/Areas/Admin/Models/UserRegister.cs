﻿using System.ComponentModel.DataAnnotations;

namespace AbuOdeh_Electromechanical.Areas.Admin.Models
{
    public class UserRegister
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
