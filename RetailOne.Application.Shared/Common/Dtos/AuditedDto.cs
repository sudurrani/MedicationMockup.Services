using MedicationMockup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Common.Dtos
{
    public class AuditedDto
    {
        //public long Id { get; set; } = 0;
        //public long CreatedBy { get; set; } = 0;
        //public DateTime? CreatedDate { get; set; }
        //public long? UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        //public long? DeletedBy { get; set; }
        //public DateTime? DeletedDate { get; set; }
        //public bool IsDeleted { get; set; } = false;
        public long Id { get; set; } = 0;
        public long? CreatedById { get; set; }
        //public virtual User? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long? UpdatedById { get; set; }
        //public virtual User? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? DeletedById { get; set; }
        //public virtual User? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
