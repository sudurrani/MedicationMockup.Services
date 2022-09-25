using MedicationMockup.Application.Shared.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Application.Shared.Dtos.User
{
    public class UserLoginOutputDto //: AuditedDto
    {
        public long Id { get; set; }
       
        public string DisplayName { get; set; } = null!;

        public string Username { get; set; } = null!;
        //public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        //public DateTime DOB { get; set; }

        public string? Email { get; set; }
        //public string? Phone { get; set; }

        //public long CountryId { get; set; }
        //public CountryIncludeOutputDto Country { get; set; }
        public string? ProfileImagePath { get; set; }

        public string? ProfileImageAbsolutePath { get; set; }
        public string? ProfileImageRelatvePath { get; set; }
        public string? ProfileImageOrignalName { get; set; }
        public string? ProfileImageNewName { get; set; }
        public string? ProfileImageExtension { get; set; }



        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;      
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? DeletedById { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
