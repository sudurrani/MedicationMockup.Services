using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Models.User
{
    public class UserLoginModel
    {
        
        [Required]
        [MaxLength(16)]
        //[MinLength(6)]
        public string Username { get; set; } = "shahid";//null!;

        [Required]
        [MaxLength(64)]
        //[MinLength(8)]
        public string Password { get; set; } = "shahid@1";//null!;
    }
}
