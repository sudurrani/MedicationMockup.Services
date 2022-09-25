using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Models.Medication
{
    public class MedicationCreateModel
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
