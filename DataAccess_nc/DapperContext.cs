
using System.Data;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace DataAccess
{
    public class DapperContext : IConnection
    {
        private readonly string? connectionString;

        public DapperContext()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            connectionString = configurationBuilder.GetConnectionString("SqlConnection");
        }


        public IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
