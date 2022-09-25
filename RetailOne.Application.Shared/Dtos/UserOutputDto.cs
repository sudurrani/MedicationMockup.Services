using MedicationMockup.Application.Shared.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Dtos.User
{
    public class UserOutputDto : AuditedDto
    {
        
        public string DisplayName { get; set; } = null!;

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;        

        public string? PIN { get; set; }
     

    }
}
