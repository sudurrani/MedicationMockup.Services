using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Dtos.User
{
    public class UserCreatedByIncludeOutputDto
    {
        public int? Id { get; set; }
        public string? DisplayName { get; set; }
        public string PIN { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
