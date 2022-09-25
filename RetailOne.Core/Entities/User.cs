using MedicationMockup.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Core.Entities
{
    public class User //: AuditedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(6)]
        public string DisplayName{ get; set; } = null!;

        [Required]
        [MaxLength(16)]
        [MinLength(6)]
        public string Username { get; set; } = null!;

        //[Required]
        [MaxLength(5)]
        [MinLength(5)]
        public string? PIN { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(8)]
        public string Password { get; set; } = null!;

     
        public long CreatedById { get; set; }        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long? UpdatedById { get; set; }        
        public DateTime? UpdatedDate { get; set; }
        public long? DeletedById { get; set; }        
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;

        //F.K
        public ICollection<Medication>? CreatedMedications { get; set; }
        public ICollection<Medication>? UpdatedMedications { get; set; }
        public ICollection<Medication>? DeletedMedications { get; set; }
        



    }
}
