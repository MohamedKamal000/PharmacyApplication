
using System.Data;
using System.Data.SqlClient;
using DataAccess.Interfaces;
namespace DataAccess
{
    public class DapperContext : IConnection
    {
        private readonly string connectionString;

        public DapperContext()
        {
            connectionString = "Server=.;Database=PharmacyApplication;User Id=sa;Password=sa123456;";
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
