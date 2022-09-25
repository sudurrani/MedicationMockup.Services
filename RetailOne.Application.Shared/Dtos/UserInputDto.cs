using MedicationMockup.Application.Shared.Common.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Dtos.User
{
    public class UserInputDto : AuditedDto
    {
        [Required]
        
        public string DisplayName { get; set; } = null!;

        [Required]
        
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = null!;

       



    }
}
