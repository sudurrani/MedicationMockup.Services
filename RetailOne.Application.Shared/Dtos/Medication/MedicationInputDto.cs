using MedicationMockup.Application.Shared.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Dtos.Medication
{
    public class MedicationInputDto : AuditedDto
    {
        public DateTime PrescriptionDate { get; set; }
        
        public string MedicationName { get; set; } = null!;

        
        public decimal Dosage { get; set; }
    }
}
