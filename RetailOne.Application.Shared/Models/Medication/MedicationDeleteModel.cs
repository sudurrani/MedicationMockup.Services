using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Models.Medication
{
    public class MedicationDeleteModel
    {
        [Required]
        public long Id { get; set; }
    }
}
