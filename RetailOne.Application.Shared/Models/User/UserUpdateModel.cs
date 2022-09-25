using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Models.User
{
    public class UserUpdateModel
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(6)]
        public string DisplayName { get; set; } = null!;

        [Required]
        [MaxLength(5)]
        [MinLength(5)]
        public string? PIN { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(8)]
        public string Password { get; set; } = null!;
        
    }
}
