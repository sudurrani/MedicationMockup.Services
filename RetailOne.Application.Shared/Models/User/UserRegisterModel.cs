using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Models.User
{
    public class UserRegisterModel
    {
        [Required]
        [MaxLength(16)]
        [MinLength(6)]
        public string DisplayName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string Username { get; set; } = null!;


        //[Required]
        [MaxLength(5)]
        [MinLength(5)]
        public string? PIN { get; set; }


        //[Required]
        //[MaxLength(100)]
        //public string Username { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string Password { get; set; } = null!;       

        public IFormFile? Image { get; set; }
    }
}
