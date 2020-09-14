using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SimpleApiAcceptedTests.Repository
{
    public class ContextDapper
    {
        private readonly IConfiguration _configuration;

        public ContextDapper(IConfiguration confiruation) => _configuration = confiruation;

        public IDbConnection Connection => new SqlConnection(_configuration.GetConnectionString("ApiConnection"));
    }
}
