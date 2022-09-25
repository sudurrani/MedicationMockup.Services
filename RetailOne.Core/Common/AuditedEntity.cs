using MedicationMockup.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Core.Common
{
    public class AuditedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long CreatedById { get; set; }
        public virtual User CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long? UpdatedById { get; set; }
        public virtual User? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? DeletedById { get; set; }
        public virtual User? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
