using MedicationMockup.Application.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Dtos.Medication
{
    public class MedicationOutputDto
    {
        public long Id { get; set; }

        public DateTime PrescriptionDate { get; set; }

        public string MedicationName { get; set; } = null!;
        public decimal Dosage { get; set; }

        public UserCreatedByIncludeOutputDto CreatedBy { get; set; } = new UserCreatedByIncludeOutputDto();
        public UserUpdatedByIncludeOutputDto UpdatedBy { get; set; } = new UserUpdatedByIncludeOutputDto();
    }
}
