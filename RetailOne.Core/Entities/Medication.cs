using MedicationMockup.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Core.Entities
{
    public class Medication : AuditedEntity
    {

        [Required]
        public DateTime PrescriptionDate { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string MedicationName { get; set; } = null!;

        [Required]
        public decimal Dosage { get; set; }


    }
}
