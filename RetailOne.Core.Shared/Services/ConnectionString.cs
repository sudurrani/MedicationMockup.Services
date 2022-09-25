using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicationMockup.Core.Shared.Services
{
    public class ConnectionString : Interfaces.IConnectionString
    {
        protected readonly IConfiguration _configuration;
        public ConnectionString(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectionString()
        {
            string connectionString = null;
            connectionString = _configuration["AppSettings:ConnectionString"];
            return connectionString;
        }
    }
}
